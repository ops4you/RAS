using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

/*
 * Classe que tem o metodo de post e read resposta do server
 */
namespace ConsoleApplication3
{
    public class PostGet
    {
        public static void Postread(string url, object json)
        {
            try
            {
                string basic="http://127.0.0.1:5000/"; //url onde ir buscar
                string final = basic + url;

                var httpWebRequest = WebRequest.CreateHttp(final);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";   

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //string json = "{ \"method\" : \"guru.test\", \"params\" : [ \"Guru\" ], \"id\" : 123 }";
                
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }
            }
            catch(WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Getread(string url)
        {
            try
            {
                string basic="http://127.0.0.1:5000/"; //url onde ir buscar
                string final = basic + url;

                var httpWebRequest = WebRequest.CreateHttp(final);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";    //method pode ser "POST", "GET"

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }
            }
            catch(WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}