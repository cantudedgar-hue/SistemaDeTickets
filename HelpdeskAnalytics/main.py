from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
import analytics

app = FastAPI(title="Helpdesk Analytics API")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"], 
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/")
def root():
    return {"status": "Helpdesk Analytics API corriendo"}

@app.get("/analytics/tickets-por-categoria")
def get_tickets_por_categoria():
    return analytics.tickets_por_categoria()

@app.get("/analytics/tiempo-resolucion")
def get_tiempo_resolucion():
    return analytics.tiempo_promedio_resolucion()

@app.get("/analytics/tickets-por-estado")
def get_tickets_por_estado():
    return analytics.tickets_por_estado()