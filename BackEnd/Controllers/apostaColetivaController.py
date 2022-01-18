import mysql.connector
from mysql.connector import Error
import userController as User


def connect_db():
    return create_server_connection("localhost", "root", "pass", "mydb")


# criar ligação com a base de dados
def create_server_connection(host_name, user_name, user_password, db_name):
    connection = None
    try:
        connection = mysql.connector.connect(
            host=host_name,
            user=user_name,
            passwd=user_password,
            database=db_name
        )
        print("MySQL Database connection successful")
    except Error as err:
        print(f"Error: '{err}'")

    return connection


# usar esta função quando for para executar uma query e só queremos saber se correu bem (200) ou mal (400)
def execute_query(connection, query):
    cursor = connection.cursor()
    try:
        cursor.execute(query)
        connection.commit()
        return 200
    except Error as err:
        return 400


# usar esta função quando for para executar uma query e receber um resultado. Neste caso, o resultado vai ser
# devolvido na variavel result
def read_query(query):
    connection = connect_db()
    cursor = connection.cursor()
    result = None
    try:
        cursor.execute(query)
        return cursor.fetchall()
    except Error as err:
        return 400


# função para o admin criar uma aposta
# oddsw -> odds de clube 1 ganhar; oddsd -> odds de draw; oddsl -> odds de clube 2 ganhar
# estado -1 aberta, -2 fechada, 0 ganha eq1, 1 draw, 2 ganha eq2
def criarAposta(clube1, clube2, oddsw, oddsd, oddsl, desporto, data_inicio="2022-01-01", data_fim="2022-02-01"):
    connection = connect_db()

    query = f'''
        INSERT INTO apostaColetiva (estado, clube1, clube2, oddsw, oddsd, oddsl, desporto, data_inicio, data_fim) 
        VALUES (-1, '{clube1}', '{clube2}', {oddsw}, {oddsd}, {oddsl}, {desporto}, {data_inicio}, {data_fim});
    '''

    return execute_query(connection, query)


# função que devolve todas as aposta abertas (estado = 1)
def getApostas():
    connection = connect_db()
    query = f'''
        SELECT * FROM apostaColetiva WHERE estado = 1;
    '''

    return read_query(query)


# função quando um user quiser fazer uma aposta
# resultado = 0 se clube 1 ganhar, 1 se draw, 2 se clube 2 ganhar
def fazerAposta(user_id, apostaColetiva_id, resultado, moeda_id, valor):
    connection = connect_db()
    query = f'''
        INSERT INTO userApostaColectiva (user_id, apostaColetiva_id, resultado, moeda_id, valor)
        VALUES ({user_id},{apostaColetiva_id}, {resultado}, {moeda_id},{valor})
    '''

    return execute_query(connection, query)


def closeAposta(aposta_id, result):
    connection = connect_db()

    query = f'''
            UPDATE aposta SET resultado={result} WHERE id={aposta_id};
        '''

    execute_query(connection, query)

    query2 = f'''
            SELECT userApostaColetiva.* FROM userApostaColetiva,apostaComposta
            WHERE apostaComposta.aposta_id={aposta_id} AND apostaComposta.userApostaColetiva_id = userApostaColetiva.aspotaColetiva_id
        '''
    result = read_query(query2)

    for x in result:
        checkapostacoletiva(x[0])


def checkapostacoletiva(apostaColetiva_id):
    query = f'''
            SELECT * FROM apostaComposta WHERE userApostaColetiva_id={apostaColetiva_id};
        '''

    result = read_query(query)
    mult = 1
    for x in result:
        query2 = f'''
                    SELECT * FROM aposta WHERE id={x[3]};
            '''

        result2 = read_query(query2)

        # multiplies the current multiplyer with if he won the bet or not with the ods
        mult = mult * (x[1] == result2[1]) * result2[3 + x[1]]

    User.addcredito(result[2], result[3] * mult, result[1])
