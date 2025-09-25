using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DReI.BackWeb.Models.Responses;

namespace DReI.BackWeb.Services.Utils
{
    public class RestService
	{
		#region "Funciones"

		private static string ConsumirGET(string url, params object[] parametrosGET)
		{
			return ConsumirGET(url, "", parametrosGET);
		}

		public static string ConsumirGET(string url, string token, params object[] parametrosGET)
		{
			string Respuesta = "";

			url = ObtenerUrl(url, parametrosGET);

			WebRequest request = System.Net.HttpWebRequest.Create(url);
			if (token != "")
				request.Headers.Add("Mu_Token", token);

			using (WebResponse response = request.GetResponse())
			{
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					Respuesta = reader.ReadToEnd();
				}
			}

			return Respuesta;
		}

		public static HttpResponseMessage ConsumirGET_HttpResponseMessage(string url, string token, params object[] parametrosGET)
		{
			url = ObtenerUrl(url, parametrosGET);

			WebRequest request = WebRequest.Create(url);
			HttpResponseMessage Respuesta;

			if (token != "")
			{
				request.Headers.Add("Mu_Token", token);
			}
			request.ContentType = "application/json; charset=utf-8";
			request.Method = "GET";

			var httpResponse = (HttpWebResponse)request.GetResponse();
			var streamReader = new StreamReader(httpResponse.GetResponseStream());
			Respuesta = new HttpResponseMessage(HttpStatusCode.OK);
			Respuesta.Content = new StreamContent(streamReader.BaseStream);
			Respuesta.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = "Informe.pdf"
			};
			Respuesta.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

			return Respuesta;
		}

		public static string ConsumirPOST(string url, object parametroPOST, params object[] parametrosGET)
		{
			return ConsumirPOST(url, null, parametroPOST, parametrosGET);
		}

		public static string ConsumirPOST(string url, string token, object parametroPOST, params object[] parametrosGET)
		{
			url = ObtenerUrl(url, parametrosGET);

			WebRequest request = WebRequest.Create(url);
			if (token != "")
			{
				request.Headers.Add("Mu_Token", token);
			}
			request.ContentType = "application/json; charset=utf-8";
			request.Method = "POST";

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(parametroPOST);

				streamWriter.Write(json);
				streamWriter.Flush();
			}

			var httpResponse = (HttpWebResponse)request.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();

				return result;
			}
		}

		public static HttpResponseMessage ConsumirPOST_HttpResponseMessage(string url, string token, object parametroPOST, params object[] parametrosGET)
		{
			url = ObtenerUrl(url, parametrosGET);

			WebRequest request = WebRequest.Create(url);
			HttpResponseMessage Respuesta;

			if (token != "")
			{
				request.Headers.Add("Mu_Token", token);
			}
			request.ContentType = "application/json; charset=utf-8";
			request.Method = "POST";

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(parametroPOST);

				streamWriter.Write(json);
				streamWriter.Flush();
			}

			var httpResponse = (HttpWebResponse)request.GetResponse();
			var streamReader = new StreamReader(httpResponse.GetResponseStream());
			Respuesta = new HttpResponseMessage(HttpStatusCode.OK);
			Respuesta.Content = new StreamContent(streamReader.BaseStream);
			Respuesta.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = "Informe.pdf"
			};
			Respuesta.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

			return Respuesta;
		}

		#region POST

		public static dynamic POST_Generico(string url, string token, object parametroPOST, params object[] parametrosGET)
		{
			return JObject.Parse(ConsumirPOST(url, token, parametroPOST, parametrosGET));
		}

		public static T POST_Generico<T>(string url, string token, object parametroPOST, params object[] parametrosGET)
		{
			return JsonConvert.DeserializeObject<T>(ConsumirPOST(url, token, parametroPOST, parametrosGET));
		}

		public static Respuesta POST(string url, string token, object parametroPOST, params object[] parametrosGET)
		{
			Respuesta Rta = JsonConvert.DeserializeObject<Respuesta>(ConsumirPOST(url, token, parametroPOST, parametrosGET));

			return Rta;
		}

		public static Respuesta POST<T>(string url, string token, object parametroPOST, params object[] parametrosGET)
		{
			Respuesta Rta = JsonConvert.DeserializeObject<Respuesta>(ConsumirPOST(url, token, parametroPOST, parametrosGET));

			Rta.Resultado = JsonConvert.DeserializeObject<T>(Rta.Resultado.ToString());

			return Rta;
		}

		/// <summary>
		/// Hace un POST a una url con un formulario como si fuera un form HTML.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="formulario"></param>
		/// <returns></returns>
		public static string PostFormulario(string url, string formulario)
		{
			//Preparo la llamada a la página
			WebRequest Request = WebRequest.Create(url);
			Request.Method = "POST";
			Request.ContentType = "application/x-www-form-urlencoded";

			byte[] Datos = Encoding.ASCII.GetBytes(formulario);
			Request.ContentLength = Datos.Length;

			//Adjunto el formulario a la llamada
			Stream RequestStream = Request.GetRequestStream();
			RequestStream.Write(Datos, 0, Datos.Length);
			RequestStream.Close();

			//Proceso la respuesta
			WebResponse Response = Request.GetResponse();
			Stream ResponseStream = Response.GetResponseStream();
			StreamReader Lector = new StreamReader(ResponseStream, Encoding.Default);

			string Cuerpo = Lector.ReadToEnd();

			Lector.Close();
			ResponseStream.Close();
			Response.Close();

			return Cuerpo;
		}

		public static string PostFormulario(string url, Dictionary<string, object> formulario)
		{
			//Preparo la llamada a la página
			WebRequest Request = WebRequest.Create(url);
			Request.Method = "POST";
			string ContentType;

			byte[] Datos = CrearCuerpoDeFormulario(formulario, out ContentType);
			Request.ContentType = ContentType;
			Request.ContentLength = Datos.Length;

			//Adjunto el formulario a la llamada
			Stream RequestStream = Request.GetRequestStream();
			RequestStream.Write(Datos, 0, Datos.Length);
			RequestStream.Close();

			//Proceso la respuesta
			WebResponse Response = Request.GetResponse();
			Stream ResponseStream = Response.GetResponseStream();
			StreamReader Lector = new StreamReader(ResponseStream, Encoding.Default);

			string Cuerpo = Lector.ReadToEnd();

			Lector.Close();
			ResponseStream.Close();
			Response.Close();

			return Cuerpo;
		}

		private static byte[] CrearCuerpoDeFormulario(Dictionary<string, object> formulario, out string contentType)
		{
			Encoding encoding = Encoding.UTF8;

			//Creo un formulario para postear
			string GuidFormulario = string.Format("----------{0:N}", Guid.NewGuid());
			contentType = "multipart/form-data; boundary=" + GuidFormulario;

			Stream formDataStream = new MemoryStream();
			bool needsCLRF = false;

			foreach (var param in formulario)
			{

				if (needsCLRF)
					formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

				needsCLRF = true;

				string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
						GuidFormulario,
						param.Key,
						param.Value);
				formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
			}

			// Add the end of the request.  Start with a newline  
			string Pie = "\r\n--" + GuidFormulario + "--\r\n";
			formDataStream.Write(encoding.GetBytes(Pie), 0, encoding.GetByteCount(Pie));

			// Dump the Stream into a byte[]  
			formDataStream.Position = 0;
			byte[] formData = new byte[formDataStream.Length];
			formDataStream.Read(formData, 0, formData.Length);
			formDataStream.Close();

			return formData;
		}

		#endregion



		#region GET

		public static dynamic GET_Generico(string url, string token, params object[] parametrosGET)
		{
			return JObject.Parse(ConsumirGET(url, token, parametrosGET));
		}

		public static T GET_Generico<T>(string url, string token, params object[] parametrosGET)
		{
			JsonSerializerSettings Configuracion = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore
			};

			return JsonConvert.DeserializeObject<T>(ConsumirGET(url, token, parametrosGET), Configuracion);
		}

		public static Respuesta GET(string url, string token, params object[] parametrosGET)
		{
			Respuesta Rta = JsonConvert.DeserializeObject<Respuesta>(ConsumirGET(url, token, parametrosGET));

			return Rta;
		}

		public static Respuesta GET<T>(string url, string token, params object[] parametrosGET)
		{
			Respuesta Rta = JsonConvert.DeserializeObject<Respuesta>(ConsumirGET(url, token, parametrosGET));

			try
			{
				Rta.Resultado = JsonConvert.DeserializeObject<T>(Rta.Resultado.ToString());

			}
			catch (Exception)
			{
				Rta.Resultado = null;
			}

			return Rta;
		}

		#endregion


		public static string ObtenerUrl(string url, params object[] parametrosGET)
		{
			if (parametrosGET == null || parametrosGET.Length == 0)
			{
				return url;
			}
			else
			{
				List<string> ListaParam = new List<string>();
				for (int i = 0; i < parametrosGET.Length; i++)
				{
					ListaParam.Add(ObtenerValorString(parametrosGET[i]));
				}

				if (url.Substring(url.Length - 1) != "/")
					url += "/";

				return url + String.Join("/", ListaParam);
			}
		}

		private static string ObtenerValorString(object o)
		{
			string valor = "";

			switch (Type.GetTypeCode(o.GetType()))
			{
				case TypeCode.Boolean:
					if ((bool)o)
						valor = "true";
					else
						valor = "false";
					break;
				case TypeCode.DateTime:
					DateTime d = ((DateTime)o);
					if (d == d.Date)
						valor = d.ToString("yyyy-MM-dd");
					else
						valor = d.ToString("yyyy-MM-dd HH:mm:ss");
					break;
				case TypeCode.Decimal:
				case TypeCode.Double:
					valor = o.ToString().Replace(",", ".");
					break;
				case TypeCode.String:
					valor = Uri.EscapeDataString(o.ToString());
					break;
				default:
					valor = o.ToString();
					break;
			}

			return valor;
		}

		#endregion

	}
}