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
			CreateDirectoryRecursively(path);
			File.WriteAllText(path + filename,data);
			
		}

		public static void CreteCSVFromQuestionnaire(string basepath, Questionnaire questionnaire) {
			string dir = basepath + questionnaire.title + "/";
			string fullPath = dir + questionnaire.title + ".csv";
			
			if(!Directory.Exists(dir)) {
				CreateDirectoryRecursively(dir);
				//Directory.CreateDirectory(dir);
			}
			if(!File.Exists(fullPath)) {
				string table = CreateCSVStringFromQuestionnaire(questionnaire);
				File.WriteAllText(fullPath,table);
			} else {
				Dictionary<string,string> columns = CreateDictFromQuestionnaire(questionnaire);
				string values = string.Join(";",columns.Values) + "\n";
				File.AppendAllText(fullPath,values);
			}
			
		}

		public static Dictionary<string, string> CreateDictFromQuestionnaire(Questionnaire questionnaire)
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
					string qKey = questionnaire.title + "_" + key;
					columns.Add(qKey,responses[key]);
				}
			}
			return columns;
		}

		public static string CreateCSVStringFromQuestionnaire(Questionnaire questionnaire)
		{
			Dictionary<string,string> columns = CreateDictFromQuestionnaire(questionnaire);


			string header = string.Join(";",columns.Keys);
			string values = string.Join(";",columns.Values) + "\n";

			string output = string.Join("\n",header,values);
			return output;
		}

		public static void CreateDirectoryRecursively(string path)
		{
			string[] pathParts = path.Split('/');

			for(int i = 0; i < pathParts.Length; i++) {
				if(i > 0)
					pathParts[i] = Path.Combine(pathParts[i - 1],pathParts[i]);

				if(!Directory.Exists(pathParts[i]))
					Directory.CreateDirectory(pathParts[i]);
			}
		}

	}

}
