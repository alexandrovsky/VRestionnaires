using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
namespace VRestionnaire {

	public static class VRestionnairePersistence {


		public static string GenerateFilename(Questionnaire questionnaire)
		{
			long utcNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			return questionnaire.title
				+ "_" + questionnaire.participantId
				+ "_" + questionnaire.condition
				+ "_" + utcNow
				+ ".csv";
		
		}

		public static void WriteFile(string path, string filename, string data)
		{
			File.WriteAllText(path + filename,data);
		}


		public static string CreateCSVTableFromQuestionnaire(Questionnaire questionnaire)
		{

			Dictionary<string,string> columns = new Dictionary<string,string>() {
				{ "participantId", questionnaire.participantId },
				{ "condition", questionnaire.condition },
				{ "title", questionnaire.title },
				{ "startUtcTime", questionnaire.startUtcTime.ToString() },
				{ "endUtcTime", questionnaire.endUtcTime.ToString() }
			};


			foreach(Question q in questionnaire.questions) {
				Dictionary<string,string> responses = q.Export();
				foreach(string key in responses.Keys) {
					columns.Add(key,responses[key]);
				}
			}

			string header = string.Join(";",columns.Keys);
			string values = string.Join(";",columns.Values);

			string output = string.Join("\n",header,values);
			return output;
		}

	}

}
