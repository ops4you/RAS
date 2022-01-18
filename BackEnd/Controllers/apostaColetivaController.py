import mysql.connector
from mysql.connector import Error
from BackEnd.Controllers import userController as User


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
        INSERT INTO mydb.aposta (estado, clube1, clube2, oddsw, oddsd, oddsl, desporto, data_inicio, data_fim) 
        VALUES (-1, '{clube1}', '{clube2}', {oddsw}, {oddsd}, {oddsl}, '{desporto}', '{data_inicio}', '{data_fim}');
    '''

    return str(execute_query(connection, query))


# função que devolve todas as aposta abertas (estado = -1)
def getApostas():
    query = f'''
        SELECT * FROM mydb.aposta WHERE estado = -1;
    '''

    lista = read_query(query)

    result = {}
    print(lista)

    for x in lista:
        print(x)
        tmp = {x[0]: {
            "id": x[0],
            "clube1": x[2],
            "clube2": x[3],
            "odsw": x[4],
            "odsd": x[5],
            "odsl": x[6],
            "desporto": x[7]
        }}
        result.update(tmp)

    return result


# função quando um user quiser fazer uma aposta
# resultado = 0 se clube 1 ganhar, 1 se draw, 2 se clube 2 ganhar
# em teoria se aposta_id fosse uma lista de apostas tbm funcionava ( given que tbm tens uma lista de resultados)
def fazerAposta(user_id, aposta_id, resultado, moeda_id, valor):
    connection = connect_db()
    print("TESTE 97")

    if ("0" == User.removecredito(moeda_id, valor, user_id)):
        return "400"

    print(user_id)
    print(moeda_id)
    print(valor)
    query = f'''
            INSERT INTO mydb.userApostaColectiva (user_id, moeda_id, valor)
            VALUES ({user_id}, {moeda_id}, {valor})
        '''

    if 400 == execute_query(connection, query):
        print("TESTE 108")
        return "400"



    query2 = f'''
            SELECT MAX( id ) FROM mydb.userApostaColectiva;

        '''
    print("TESTE 116")
    # se de erro aqui tentar tirar o [0] e se tbm der erro tentrar trocar po [0][0]
    print("Fazer aposta -> q2")
    apcid = read_query(query2)
    print(apcid[0][0])
    print(apcid)
    print("testeeeeee")
    query3 = f'''
              INSERT INTO mydb.aposta_composta (resultado, userApostaColectiva, aposta_id)
              VALUES ({resultado}, {apcid[0][0]},{aposta_id})
          '''
    return str(execute_query(connection, query3))


def closeAposta(aposta_id, result):
    connection = connect_db()

    query = f'''
            UPDATE mydb.aposta SET resultado={result} WHERE id={aposta_id};
        '''

    if execute_query(connection, query) == 400:
        return "400"

    query2 = f'''
            SELECT * FROM mydb.userApostaColectiva,mydb.aposta_composta
            WHERE aposta_composta.aposta_id={aposta_id} 
            AND aposta_composta.userApostaColectiva_id = userApostaColectiva.id
        '''
    result = read_query(query2)

    if result == 400:
        return "400"

    for x in result:
        checkapostacoletiva(x[0])

    return "200"


def checkapostacoletiva(apostaColetiva_id):
    query = f'''
            SELECT * FROM mydb.aposta_composta WHERE userApostaColectiva_id={apostaColetiva_id};
        '''

    result = read_query(query)
    mult = 1
    for x in result:
        query2 = f'''
                    SELECT * FROM mydb.aposta WHERE id={x[3]};
            '''

        result2 = read_query(query2)

        # multiplies the current multiplyer with if he won the bet or not with the ods
        mult = mult * (x[1] == result2[1]) * result2[3 + x[1]]
    if mult > 0:
        User.addcredito(result[2], result[3] * mult, result[1])
