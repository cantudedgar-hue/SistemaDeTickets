import pandas as pd
from database import get_connection

def tickets_por_categoria():
    conn = get_connection()
    query = """
        SELECT c.name AS categoria, COUNT(t.id) AS total_tickets
        FROM tickets t
        LEFT JOIN categories c ON t.category_id = c.id
        GROUP BY c.name
        ORDER BY total_tickets DESC
    """
    df = pd.read_sql(query, conn)
    conn.close()
    return df.to_dict(orient="records")


def tiempo_promedio_resolucion():
    conn = get_connection()
    query = """
        SELECT id, created_at, closed_at
        FROM tickets
        WHERE closed_at IS NOT NULL
    """
    df = pd.read_sql(query, conn)
    conn.close()

    if df.empty:
        return {"promedio_horas": 0, "tickets_cerrados": 0}

    df["created_at"] = pd.to_datetime(df["created_at"])
    df["closed_at"] = pd.to_datetime(df["closed_at"])
    df["horas_resolucion"] = (df["closed_at"] - df["created_at"]).dt.total_seconds() / 3600

    return {
        "promedio_horas": round(df["horas_resolucion"].mean(), 2),
        "tickets_cerrados": len(df)
    }


def tickets_por_estado():
    conn = get_connection()
    query = """
        SELECT status, COUNT(*) AS total
        FROM tickets
        GROUP BY status
    """
    df = pd.read_sql(query, conn)
    conn.close()
    return df.to_dict(orient="records")