using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UiPath
{
    public static class OrchestratorAPIHelper
    {
        static string baseUrl = "https://platform.uipath.com";

        public static string GetToken(object loginModel)
        {
            string token = string.Empty;
            try
            {
                var client = new RestClient(baseUrl + "/api/account/authenticate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");

                string jsonLoginModel = JsonConvert.SerializeObject(loginModel, Formatting.Indented);
                request.AddParameter("loginModel", jsonLoginModel, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string isSuccess = GetValue(response.Content, "success");
                if ((int)response.StatusCode == 200 && isSuccess == "True")
                {
                    //Console.WriteLine("Authenticate - Success : " + response.StatusCode);
                    token = GetValue(response.Content, "result");
                }
                else
                {
                    Console.WriteLine("Authenticate - Failed : " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }

            return token;
        }

        public static RobotDto[] GetListOfRobots(string token)
        {
            RobotDto[] robots = null;
            var client = new RestClient(baseUrl + "/odata/Robots");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                //Console.WriteLine("GetListOfRobots - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int robotCount = int.Parse(jContent["@odata.count"].ToString());
                robots = JsonConvert.DeserializeObject<RobotDto[]>(jContent["value"].ToString());

                //Console.WriteLine("No. of Robots Found : " + robotCount);
                //foreach (var robot in robots)
                //{
                //    //string jsonRobot= JsonConvert.SerializeObject(robot, Formatting.Indented);
                //    //Console.WriteLine(jsonRobot);

                //    Console.WriteLine(robot.Name);
                //}
            }
            else
            {
                Console.WriteLine("GetListOfRobots - Failed : " + response.StatusCode);
            }

            return robots;
        }

        public static RobotDto GetRobot(RobotDto[] robots, string robotName)
        {
            RobotDto robot = robots.SingleOrDefault(r => r.Name == robotName);
            //string jsonRobot = JsonConvert.SerializeObject(robot, Formatting.Indented);
            //Console.WriteLine(jsonRobot);
            return robot;
        }

        public static RobotDto[] GetRobots(RobotDto[] robots, string envName)
        {
            RobotDto[] matchRobots = robots.Where(r => r.RobotEnvironments.Contains(envName)).ToArray();
            string jsonRobots = JsonConvert.SerializeObject(matchRobots, Formatting.Indented);
            Console.WriteLine(jsonRobots);
            return matchRobots;
        }

        public static ReleaseDto[] GetListOfReleases(string token)
        {
            ReleaseDto[] releases = null;
            var client = new RestClient(baseUrl + "/odata/Releases?$count=false");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                //Console.WriteLine("GetListOfReleases - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int releaseCount = int.Parse(jContent["@odata.count"].ToString());
                releases = JsonConvert.DeserializeObject<ReleaseDto[]>(jContent["value"].ToString());

                //Console.WriteLine("No. of Releases Found : " + releaseCount);
                //foreach (var release in releases)
                //{
                //    string jsonRelease = JsonConvert.SerializeObject(release, Formatting.Indented);
                //    Console.WriteLine(jsonRelease);

                //    //Console.WriteLine(release.ProcessKey);
                //}
            }
            else
            {
                Console.WriteLine("GetListOfReleases - Failed : " + response.StatusCode);
            }

            return releases;
        }

        public static ReleaseDto GetReleaseByProcessName(ReleaseDto[] releases, string processName)
        {
            ReleaseDto release = releases.SingleOrDefault(r => r.ProcessKey == processName);
            string jsonRelease = JsonConvert.SerializeObject(release, Formatting.Indented);
            Console.WriteLine(jsonRelease);
            return release;
        }

        public static ReleaseDto GetReleaseByEnvName(ReleaseDto[] releases, string envName)
        {
            ReleaseDto release = releases.SingleOrDefault(r => r.EnvironmentName == envName);
            string jsonRelease = JsonConvert.SerializeObject(release, Formatting.Indented);
            Console.WriteLine(jsonRelease);
            return release;
        }

        public static bool AddQueue(string token, string queueName, object specificContent)
        {
            bool result = false;
            var client = new RestClient(baseUrl + "/odata/Queues/UiPathODataSvc.AddQueueItem");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            object someNull = null;

            var data = new
            {
                itemData = new
                {
                    Name = queueName,
                    Priority = "Normal",
                    SpecificContent = specificContent,
                    DeferDate = someNull,
                    DueDate = someNull
                }
            };

            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            request.AddParameter("data", jsonData, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 201)
            {
                //Console.WriteLine("AddQueue - Success : " + response.StatusCode);
                result = true;
            }
            else
            {
                Console.WriteLine("AddQueue - Failed : " + response.StatusCode);
                result = false;
            }

            return result;
        }

        public static JobDto[] ExecuteRobot(string token, string releaseKey, int[] robotIDs, object inputArguments = null)
        {
            JobDto[] jobs = null;
            var data = new
            {
                startInfo = new
                {
                    ReleaseKey = releaseKey,
                    RobotIds = robotIDs,
                    NoOfRobots = 0,
                    Strategy = "Specific",
                    Source = "Manual",
                    //InputArguments = inputArguments
                }
            };

            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            //Console.WriteLine(jsonData);

           var client = new RestClient(baseUrl + "/odata/Jobs/UiPath.Server.Configuration.OData.StartJobs");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("data", jsonData, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 201)
            {
                //Console.WriteLine("ExecuteRobot - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                jobs = JsonConvert.DeserializeObject<JobDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("ExecuteRobot - Failed : " + response.StatusCode);
            }

            return jobs;
        }

        public static void StopRobot(string token, int jobId, string strategy)
        {
            var data = new
            {
                Strategy = strategy
            };

            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            Console.WriteLine(jsonData);

            var client = new RestClient(baseUrl + "/odata/Jobs(" + jobId + ")/UiPath.Server.Configuration.OData.StopJob");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("data", jsonData, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 201)
            {
                Console.WriteLine("StopRobot - Success : " + response.StatusCode);
            }
            else
            {
                Console.WriteLine("StopRobot - Failed : " + response.StatusCode);
            }
        }

        private static string GetValue(string json, string key)
        {
            JObject jContent = JObject.Parse(json);
            string value = jContent[key].ToString();
            return value;
        }

        public static AssetDto[] GetAssets(string token)
        {
            AssetDto[] assets = null;
            var client = new RestClient(baseUrl + "/odata/Assets");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                Console.WriteLine("GetAssets - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int logCount = int.Parse(jContent["@odata.count"].ToString());
                assets = JsonConvert.DeserializeObject<AssetDto[]>(jContent["value"].ToString());

                Console.WriteLine("No. of Assets Found : " + logCount);
                foreach (var asset in assets)
                {
                    string jsonLog = JsonConvert.SerializeObject(asset, Formatting.Indented);
                    Console.WriteLine(jsonLog);
                }
            }
            else
            {
                Console.WriteLine("GetAssets - Failed : " + response.StatusCode);
            }

            return assets;
        }

        public static JobDto[] GetJobHistory(string token, string processKey)
        {
            JobDto[] jobs = null;
            var client = new RestClient(baseUrl + "/odata/Jobs?$filter=((contains(Release/ProcessKey,'" + processKey + "')))&$expand=Robot,Release&$orderby=CreationTime%20desc&$top=20");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                Console.WriteLine("GetJobDetails - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                jobs = JsonConvert.DeserializeObject<JobDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetJobDetails - Failed : " + response.StatusCode);
            }

            return jobs;
        }

        public static JobDto GetJobDetails(string token, int jobId)
        {
            JobDto job = null;
            var client = new RestClient(baseUrl + "/odata/Jobs(" + jobId + ")?$expand=Robot,Release");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                Console.WriteLine("GetJobDetails - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                string Key = jContent["Key"].ToString();
                string StartTime = jContent["StartTime"].ToString();
                string EndTime = jContent["EndTime"].ToString();
                string State = jContent["State"].ToString();
                string Source = jContent["Source"].ToString();
                string BatchExecutionKey = jContent["BatchExecutionKey"].ToString();
                string Info = jContent["Info"].ToString();
                string CreationTime = jContent["CreationTime"].ToString();
                string StartingScheduleId = jContent["StartingScheduleId"].ToString();
                int Id = int.Parse(jContent["Id"].ToString());

                ReleaseDto release = JsonConvert.DeserializeObject<ReleaseDto>(jContent["Release"].ToString());
                RobotDto robot = JsonConvert.DeserializeObject<RobotDto>(jContent["Robot"].ToString());

                job = new JobDto()
                {
                    Key = Key,
                    StartTime = StartTime,
                    EndTime = EndTime,
                    State = State,
                    Source = Source,
                    BatchExecutionKey = BatchExecutionKey,
                    Info = Info,
                    CreationTime = CreationTime,
                    StartingScheduleId = StartingScheduleId,
                    Id = Id,
                    Release = release,
                    Robot = robot
                };
            }
            else
            {
                Console.WriteLine("GetJobDetails - Failed : " + response.StatusCode);
            }

            return job;
        }

        public static SessionDto[] GetListOfSessions(string token)
        {
            SessionDto[] sessions = null;
            var client = new RestClient(baseUrl + "/odata/Sessions?$expand=Robot");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                Console.WriteLine("GetListOfSessions - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int sessionCount = int.Parse(jContent["@odata.count"].ToString());
                sessions = JsonConvert.DeserializeObject<SessionDto[]>(jContent["value"].ToString());

                Console.WriteLine("No. of Sessions Found : " + sessionCount);
                foreach (var session in sessions)
                {
                    string jsonRobot = JsonConvert.SerializeObject(session, Formatting.Indented);
                    Console.WriteLine(jsonRobot);
                }
            }
            else
            {
                Console.WriteLine("GetListOfSessions - Failed : " + response.StatusCode);
            }

            return sessions;
        }

        public static SessionDto GetSession_byRobotName(SessionDto[] sessions, string robotName)
        {
            SessionDto session = sessions.SingleOrDefault(r => r.Robot.Name == robotName);
            return session;
        }

        public static LogDto[] GetLogs(string token, string jobKey)
        {
            LogDto[] logs = null;
            var client = new RestClient(baseUrl + "/odata/RobotLogs?$filter=(JobKey%20eq%20" + jobKey + ")&$orderby=TimeStamp%20desc");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                Console.WriteLine("GetLogs - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int sessionCount = int.Parse(jContent["@odata.count"].ToString());
                logs = JsonConvert.DeserializeObject<LogDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetLogs - Failed : " + response.StatusCode);
            }

            return logs;
        }

        public static QueueDefinitionDto[] GetQueueDefinitions(string token)
        {
            QueueDefinitionDto[] queues = null;
            string url = baseUrl + "/odata/QueueDefinitions?$top=50";
            //if(!string.IsNullOrEmpty(queueName))
            //{
            //    url += "?$filter=Name%20eq%20" + queueName;
            //}

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                //Console.WriteLine("GetQueueDefinitions - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int qCount = int.Parse(jContent["@odata.count"].ToString());
                queues = JsonConvert.DeserializeObject<QueueDefinitionDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetQueueDefinitions - Failed : " + response.StatusCode);
            }

            return queues;
        }

        public static QueueProcessingStatus[] GetQueueDefinitionByName(string token, string queueName)
        {
            QueueProcessingStatus[] queues = null;
            string url = baseUrl + "/odata/QueueProcessingRecords/UiPathODataSvc.RetrieveQueuesProcessingStatus?$filter=((contains(QueueDefinitionName,'" + queueName + "')%20or%20contains(QueueDefinitionDescription,'" + queueName + "')))&$orderby=QueueDefinitionName&$top=50";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                //Console.WriteLine("GetQueueDefinitions - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int qCount = int.Parse(jContent["@odata.count"].ToString());
                queues = JsonConvert.DeserializeObject<QueueProcessingStatus[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetQueueDefinitionByName - Failed : " + response.StatusCode);
            }

            return queues;
        }

        public static EnvironmentDto[] GetListOfEnvironments(string token)
        {
            EnvironmentDto[] environments = null;
            var client = new RestClient(baseUrl + "/odata/Environments?$expand=Robots");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                //Console.WriteLine("GetListOfEnvironments - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int sessionCount = int.Parse(jContent["@odata.count"].ToString());
                environments = JsonConvert.DeserializeObject<EnvironmentDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetListOfEnvironments - Failed : " + response.StatusCode);
            }

            return environments;
        }

        public static ProcessScheduleDto[] GetListOfSchedules(string token)
        {
            ProcessScheduleDto[] schedules = null;
            var client = new RestClient(baseUrl + "/odata/ProcessSchedules?$orderby=Name");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                //Console.WriteLine("GetListOfSchedules - Success : " + response.StatusCode);

                JObject jContent = JObject.Parse(response.Content);
                int sessionCount = int.Parse(jContent["@odata.count"].ToString());
                schedules = JsonConvert.DeserializeObject<ProcessScheduleDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetListOfSchedules - Failed : " + response.StatusCode);
            }

            return schedules;
        }

        public static QueueItemDto[] GetQueueItems(string token, int queueId)
        {
            QueueItemDto[] queueItems = null;
            string url = baseUrl + "/odata/QueueItems?$filter=(QueueDefinitionId%20eq%20" + queueId + ")&$top=50";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                JObject jContent = JObject.Parse(response.Content);
                int qCount = int.Parse(jContent["@odata.count"].ToString());
                queueItems = JsonConvert.DeserializeObject<QueueItemDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetQueueItems - Failed : " + response.StatusCode);
            }

            return queueItems;
        }

        public static QueueItemDto[] GetAllFailedQueueItems(string token, int queueId)
        {
            QueueItemDto[] queueItems = null;
            string url = baseUrl + "/odata/QueueItems?$filter=(Status%20%20eq%20%20'2'%20and%20QueueDefinitionId%20eq%20" + queueId + ")&$top=50";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                JObject jContent = JObject.Parse(response.Content);
                int qCount = int.Parse(jContent["@odata.count"].ToString());
                queueItems = JsonConvert.DeserializeObject<QueueItemDto[]>(jContent["value"].ToString());
            }
            else
            {
                Console.WriteLine("GetAllFailedQueueItems - Failed : " + response.StatusCode);
            }

            return queueItems;
        }

        public static void SetItemReviewStatus(string token, int queueId, string rowVersion)
        {
            string url = baseUrl + "/odata/QueueItems/UiPathODataSvc.SetItemReviewStatus()";

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            string jsonData = "{\"queueItems\":[{\"RowVersion\": \"" + rowVersion + "\",\"Id\":" + queueId + "}],\"status\":\"Retried\"}";
            request.AddParameter("data", jsonData, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                JObject jContent = JObject.Parse(response.Content);
            }
            else
            {
                Console.WriteLine("GetAllFailedQueueItems - Failed : " + response.StatusCode);
            }
        }
    }
}
