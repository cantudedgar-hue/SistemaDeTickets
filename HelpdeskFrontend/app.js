const API_TICKETS = "http://localhost:5000/api/Tickets";
const API_ANALYTICS = "http://localhost:8000/analytics";

async function cargarTickets() {
  const res = await fetch(API_TICKETS);
  const tickets = await res.json();

  const tbody = document.querySelector("#tickets-table tbody");
  tbody.innerHTML = tickets.map(t => `
    <tr>
      <td>${t.id}</td>
      <td>${t.title}</td>
      <td class="status-${t.status}">${t.status}</td>
      <td>${t.priority}</td>
      <td>${t.userName ?? "-"}</td>
      <td>${t.agentName ?? "-"}</td>
      <td>${t.categoryName ?? "-"}</td>
    </tr>
  `).join("");
}

async function cargarMetricas() {
  const [porEstado, porCategoria, tiempoResolucion] = await Promise.all([
    fetch(`${API_ANALYTICS}/tickets-por-estado`).then(r => r.json()),
    fetch(`${API_ANALYTICS}/tickets-por-categoria`).then(r => r.json()),
    fetch(`${API_ANALYTICS}/tiempo-resolucion`).then(r => r.json())
  ]);

  document.getElementById("estado-list").innerHTML =
    porEstado.map(e => `<li>${e.status}: ${e.total}</li>`).join("");

  document.getElementById("categoria-list").innerHTML =
    porCategoria.map(c => `<li>${c.categoria ?? "Sin categoría"}: ${c.total_tickets}</li>`).join("");

  document.getElementById("tiempo-resolucion").textContent =
    tiempoResolucion.tickets_cerrados > 0
      ? `${tiempoResolucion.promedio_horas} horas (${tiempoResolucion.tickets_cerrados} tickets cerrados)`
      : "Sin datos aún";
}

cargarTickets();
cargarMetricas();