using System.Threading.Tasks;
using Grpc.Net.Client;
using grpclienttest;
using Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Configuration;


public partial class GRpcClientTester
{
    private static Metadata? gMetadata;
    private static GrpcChannel? gChannel;
    //  private static string token ;


    public static void Main(string[] argv)
    {
        gChannel = GetChannel();
        string tkn = GetToken();
        gMetadata = GetMetadata(tkn);


        //        Console.WriteLine("Objects") ;
        //        TestObjectsServ();

       //         Console.WriteLine("Narabotkas") ;
       //         TestNarabotkasServ();
       //         Console.WriteLine("Requests") ;
       //         TestRequestsServ();
       //         Console.WriteLine("Telemetry GetByDate") ;
       //         TestTelemetryServDate();
       //         Console.WriteLine("Telemetry TestTelemetryServPeriod") ;
       //         TestTelemetryServPeriod();
       //         Console.WriteLine("Telemetry TestTelemetryServPeriodByObjKod") ;
       //         TestTelemetryServPeriodByObjKod();
       //         Console.WriteLine("Telemetry TestTelemetryTriningObj") ;
       //         TestTelemetryTriningObj();
       //         
       //         Console.WriteLine("Telemetry TestTelemetryTriningObj") ;
       //         TestCheckingTriningObj();
       // 
       //         Console.WriteLine("TestLastTrainingId") ;
       //         var lid =  TestLastTrainingId();
       //         Console.WriteLine("TestGetToken");
       //         TestGetToken();
       // 
       //         Console.WriteLine("TestGetWarning4");
       //         TestGetWarning4();
       ///// Console.WriteLine("TestUploadTelemetry");
       /// var dtt = new DateTime(2022,01,01,0,0,0) ;
       /// TestUploadTelemetry(12,3,dtt);

        Console.WriteLine("TestGetWarning4");
        TestTelemetryData(13);

        Console.WriteLine("Press any key to exit...");


      //  Console.ReadKey();
    }

    public static void TestUploadTelemetry(int oKod,int tType,DateTime dt)
    {
        string filepn = WUploadFile();
        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        var fbytes0 = File.ReadAllBytes(filepn);

        var ams1 = new MemoryStream();
       // var ams = new MemoryStream(fbytes0) ;
        using(System.IO.Compression.DeflateStream dstream = new System.IO.Compression.DeflateStream(ams1, System.IO.Compression.CompressionLevel.Optimal))
        {
             dstream.Write(fbytes0,0,fbytes0.Count()) ;   
        }

        var fbytes = ams1.ToArray() ;
        int scount = 0;
        if (fbytes != null && fbytes.Count() > 0)
        {
            var asyncfc = async () =>
            {
                using (var tcall = telemetryclient.UploadTelemetry(gMetadata))
                {
                    int i = 0;
                    int step = 5024;
                    for (i = 0; i < fbytes.Count(); i += step)
                    {
                        byte[] buf;
                        if (i < (fbytes.Count() - step))
                        {
                            buf = new byte[step];
                            Array.Copy(fbytes, i, buf, 0, step);
                        }
                        else
                        {
                            var sz = fbytes.Count() - i;
                            buf = new byte[sz];
                            Array.Copy(fbytes, i, buf, 0, sz);
                        }
                        var bara = Google.Protobuf.ByteString.CopyFrom(buf);

                        await tcall.RequestStream.WriteAsync(new GWitsml { Data = bara });
                        scount = i;
                    }
                    int objcode = oKod;
                       //stack
                       //1- kod
                       //2- type
                       //3- period y
                       //4- period m
                       //5- period d
                       //6- period h
                       //7- period m 
                    var szi = sizeof(int);
                    var kodBts = BitConverter.GetBytes(objcode);
                    var typeBts = BitConverter.GetBytes(tType);  
                    var yearBts = BitConverter.GetBytes(dt.Year);
                    var monthBts = BitConverter.GetBytes(dt.Month);
                    var dayBts = BitConverter.GetBytes(dt.Day);
                    var hourBts = BitConverter.GetBytes(dt.Hour);
                    var minBts = BitConverter.GetBytes(dt.Minute);


                    var bArKod = Google.Protobuf.ByteString.CopyFrom(kodBts);
                    var bArType = Google.Protobuf.ByteString.CopyFrom(typeBts);
                    var bArYear = Google.Protobuf.ByteString.CopyFrom(yearBts);
                    var bArMonth = Google.Protobuf.ByteString.CopyFrom(monthBts);
                    var bArDay = Google.Protobuf.ByteString.CopyFrom(dayBts);
                    var bArHour = Google.Protobuf.ByteString.CopyFrom(hourBts);
                    var bArMin = Google.Protobuf.ByteString.CopyFrom(minBts);

                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArKod });
                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArType });
                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArYear });
                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArMonth });
                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArDay });
                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArHour });
                    await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArMin });
                    
           

                    await tcall.RequestStream.CompleteAsync();
                    return await tcall.ResponseAsync;
                }
            };
            var tsk = asyncfc();

            while (tsk.Status == TaskStatus.Running)
            {
                Console.Clear();
                Console.Write($"Send total: {scount} ");
            }
            tsk.Wait();
            //it's for test when last one server have returns byte count
            //for now it return total records that was adds to server
          //  if (tsk.Status != TaskStatus.RanToCompletion || tsk.Result.Count != (fbytes.Count()+sizeof(int)*7))
          //  {
          //      throw new Exception("Wrong something");
          //  }
            Console.WriteLine($"Success {tsk.Result.Count} , time in ms : {tsk.Result.TimeTotal}");

        }
    }

public static void TestTelemetryData(int objId)
    {
        var gobclient = new ObjectsServ.ObjectsServClient(gChannel);
        var  obj = gobclient.GetById(new GObjectId {Id = objId}, gMetadata);
      
        if(obj!=null)
        {
        var errorPeriod = obj.ErrorPeriod ;  
        var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        var warningDate = obj.WarningTime.ToDateTime() ;  
        var stDt =  warningDate.AddMinutes(-1*errorPeriod/2) ;//Add(offset) ;
        var enDt =  warningDate.AddMinutes(1*errorPeriod/2) ;//.Add(offset) ;
        var startDate = ZHelper.TimeSFromDateTime(stDt);
        var endDate = ZHelper.TimeSFromDateTime(enDt);
        var period = new GTelemetryPeriodForObject { 
                                                    KodObject =  new GObjectId { Id = obj.Kod },
                                                    GTelemetryPeriod = new GTelemetryPeriod { Begin = startDate, End = endDate}
                                                    }
                                                    ;
        
        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        var reqall = telemetryclient.GetForPeriodForObjectByKod(period, gMetadata);
        if(reqall !=null && reqall.Items.Count>0)
        {
            foreach(var itm in reqall.Items)
            {
               Console.WriteLine($"{itm.Id};\t{itm.KodObject};\t{itm.Period.ToDateTime()};\t{itm.Value.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"))};") ; 
            }    
        }
        }

      
    }



    public static void TestGetWarning4()
    {
        var stDt = (new DateTime(2022, 8, 11)).AddHours(3).AddMinutes(14);
        var enDt = stDt.AddMinutes(61);
        var startDate = ZHelper.TimeSFromDateTime(stDt);
        var period = new GTelemetryPeriod { Begin = ZHelper.TimeSFromDateTime(stDt), End = ZHelper.TimeSFromDateTime(enDt) };

        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        var reqall = telemetryclient.GetForPeriodForObjectWarning4(new GTelemetryPeriodForObject { KodObject = new GObjectId { Id = 11 }, GTelemetryPeriod = period }, gMetadata);

        Console.WriteLine(@"Type1:");
        foreach (var gobj in reqall.Type1.Items)
        {
            ZHelper.Print(gobj);
        }


        Console.WriteLine(@"Type2:");
        foreach (var gobj in reqall.Type2.Items)
        {
            ZHelper.Print(gobj);
        }
    }

    public static void TestGetToken()
    {
        var tokenclient = new GGetTokensServ.GGetTokensServClient(gChannel);

        var gclaims = new GClaim[] {
             new GClaim { Uri = "/ObjectsServ/GetById"},
             new GClaim { Uri = "/NarabotkaServ/GetByObjectKod"},
             new GClaim { Uri = "/TelemetryServ/GetForPeriodForObjectByKod"},
             new GClaim { Uri = "/GGetTokensServ/GetToken"},
         };
        var claims = new GGetTokenClaims { };
        claims.Claims.AddRange(gclaims);

        var token = tokenclient.GetToken(claims, gMetadata);
        Console.WriteLine($"Token:\t \"{token.Uri}\"");
    }



    public static int TestLastTrainingId()
    {
        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        int? id = telemetryclient.GetLastTrainedRecId(new Google.Protobuf.WellKnownTypes.Empty { }, gMetadata)?.Id;
        return id ?? -1;
    }

    public static void TestCheckingTriningObj()
    {
        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        telemetryclient.CheckingObj(new GObjectId { Id = 5 }, gMetadata);
    }

    public static void TestTelemetryTriningObj()
    {
        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        int lm = TestLastTrainingId();
        telemetryclient.TrainingObj(new GObjectId { Id = 5 }, gMetadata);
        int lm1 = TestLastTrainingId();
        if (lm1 <= lm)
            throw new Exception("Error training");
    }

    public static void TestTelemetryServPeriod()
    {
        var stDt = (new DateTime(2022, 4, 1)).AddHours(10);
        var enDt = stDt.AddHours(2).AddMinutes(12);
        var startDate = ZHelper.TimeSFromDateTime(stDt);
        var period = new GTelemetryPeriod { Begin = ZHelper.TimeSFromDateTime(stDt), End = ZHelper.TimeSFromDateTime(enDt) };

        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        var reqall = telemetryclient.GetForPeriod(period, gMetadata);
        foreach (var gobj in reqall.Items)
        {
            ZHelper.Print(gobj);
        }
    }
    public static void TestTelemetryServPeriodByObjKod()
    {
        var stDt = (new DateTime(2022, 4, 1)).AddHours(15);
        var enDt = stDt.AddHours(1).AddMinutes(22);
        var startDate = ZHelper.TimeSFromDateTime(stDt);
        var period = new GTelemetryPeriod { Begin = ZHelper.TimeSFromDateTime(stDt), End = ZHelper.TimeSFromDateTime(enDt) };

        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        var reqall = telemetryclient.GetForPeriodForObjectByKod(new GTelemetryPeriodForObject { KodObject = new GObjectId { Id = 5 }, GTelemetryPeriod = period }, gMetadata);
        foreach (var gobj in reqall.Items)
        {
            ZHelper.Print(gobj);
        }
    }
    public static void TestTelemetryServDate()
    {
        var startDate = ZHelper.TimeSFromDateTime(new DateTime(2022, 1, 1));
        var telemetryclient = new TelemetryServ.TelemetryServClient(gChannel);
        var reqall = telemetryclient.GetForDate(startDate, gMetadata);
        foreach (var gobj in reqall.Items)
        {
            ZHelper.Print(gobj);
        }
    }

    public static void TestRequestsXml()
    {
        var reqclient = new RequestServ.RequestServClient(gChannel);

        var reqs = reqclient.GetByObjectKod(new GObjectId { Id = 5 }, gMetadata);
        foreach (var gobj in reqs.Items)
        {
            ZHelper.Print(gobj);
        }


       
        var gobj1 = reqclient.Post(new GRequest
        {
            Num = 143,
            Data = ZHelper.TimeSFromDateOnlyVals(2020, 12, 3),
            KodObject = 5,
            DateFrom = ZHelper.TimeSFromDateOnlyVals(2021, 11, 4),
            DateTo = ZHelper.TimeSFromDateOnlyVals(2022, 10, 3),
            Status = 4,
            DateFromFakt = ZHelper.TimeSFromDateOnlyVals(2016, 9, 3),
            DateToFakt = ZHelper.TimeSFromDateOnlyVals(2019, 10, 3),
            Comment = "Новый запрос",
        }, gMetadata);

        Console.WriteLine("!!! update");

       
    }

    public static void TestRequestsServ()
    {
        var reqclient = new RequestServ.RequestServClient(gChannel);

        var reqs = reqclient.GetByObjectKod(new GObjectId { Id = 5 }, gMetadata);
        foreach (var gobj in reqs.Items)
        {
            ZHelper.Print(gobj);
        }


        Console.WriteLine("Get all");
        var reqall = reqclient.GetAll(new grpclienttest.Empty(), gMetadata);
        foreach (var gobj in reqall.Items)
        {
            ZHelper.Print(gobj);
        }

        Console.WriteLine("!!! ByID");

        var gobj0 = reqclient.GetById(new GRequestId { Id = 2 }, gMetadata);
        ZHelper.Print(gobj0);


        Console.WriteLine("!!! post");

        var gobj1 = reqclient.Post(new GRequest
        {
            Num = 143,
            Data = ZHelper.TimeSFromDateOnlyVals(2020, 12, 3),
            KodObject = 5,
            DateFrom = ZHelper.TimeSFromDateOnlyVals(2021, 11, 4),
            DateTo = ZHelper.TimeSFromDateOnlyVals(2022, 10, 3),
            Status = 4,
            DateFromFakt = ZHelper.TimeSFromDateOnlyVals(2016, 9, 3),
            DateToFakt = ZHelper.TimeSFromDateOnlyVals(2019, 10, 3),
            Comment = "Новый запрос",
        }, gMetadata);

        Console.WriteLine("!!! update");

        var gobj2 = reqclient.Put(new GRequest
        {
            Id = 4,
            Num = 143,
            Data = ZHelper.TimeSFromDateOnlyVals(2021, 12, 4),
            KodObject = 5,
            DateFrom = ZHelper.TimeSFromDateOnlyVals(2020, 11, 5),
            DateTo = ZHelper.TimeSFromDateOnlyVals(2022, 11, 4),
            Status = 4,
            DateFromFakt = ZHelper.TimeSFromDateOnlyVals(2016, 9, 4),
            DateToFakt = ZHelper.TimeSFromDateOnlyVals(2019, 8, 4),
            Comment = "Новый запрос 123",
        }, gMetadata);

        Console.WriteLine("!!! delete");
        var gobj3 = reqclient.Delete(new GRequestId { Id = gobj1.Id }, gMetadata);



    }
    public static void TestNarabotkasServ()
    {

        var gobclient = new NarabotkaServ.NarabotkaServClient(gChannel);




        Console.WriteLine("Get by object code");
        var narab = gobclient.GetByObjectKod(new GObjectId { Id = 3 }, gMetadata);
        foreach (var gobj in narab.Items)
        {
            ZHelper.Print(gobj);
        }
        //  return ;
        Console.WriteLine("Get all");
        ///
        var objallreply = gobclient.GetAll(new grpclienttest.Empty(), gMetadata);
        foreach (var gobj in objallreply.Items)
        {
            ZHelper.Print(gobj);
        }

        Console.WriteLine("!!! ByID");
        var gobj0 = gobclient.GetById(new GObjectId { Id = 1462 }, gMetadata);
        ZHelper.Print(gobj0);

        Console.WriteLine("!!! post");

        var gobj1 = gobclient.Post(new GNarabotka
        {
            Data = ZHelper.TimeSFromDateOnlyVals(18, 12, 3),
            Val = 12,
            KodObject = 7
        }, gMetadata);

        Console.WriteLine("!!! update");

        var gobj2 = gobclient.Put(new GNarabotka
        {
            Id = 1559,
            Data = ZHelper.TimeSFromDateOnlyVals(2020, 12, 3),
            Val = 13,
            KodObject = 9
        }, gMetadata);

        Console.WriteLine("!!! delete");
        var gobj3 = gobclient.Delete(new GObjectId { Id = gobj1.Id }, gMetadata);
    }
    public static void TestObjectsServ()
    {
        var gobclient = new ObjectsServ.ObjectsServClient(gChannel);
        var objallreply = gobclient.GetAll(new grpclienttest.Empty(), gMetadata);
        foreach (var gobj in objallreply.Items)
        {
            ZHelper.Print(gobj);
        }
        Console.WriteLine("!!! ByID");
        var gobj0 = gobclient.GetById(new GObjectId { Id = 18 }, gMetadata);
        ZHelper.Print(gobj0);

        Console.WriteLine("!!! post");
        var gobj1 = gobclient.Post(new GObject
        {
            Kod = 787,
            Name = "Объект 176",
            TekNar = 211,
            LastTo = ZHelper.TimeSFromDateOnlyVals(2021, 9, 10),
            ToTime = 301,
            ToNar = 3001,
            PlanYear = 6000,
            Koef2 = 0.4f,
            Koef1 = 1.02f,
            SredNar = 9.06f,
            DateFrom = ZHelper.TimeSFromDateOnlyVals(2022, 7, 12),
            NarFrom = 1,
            NextTo = ZHelper.TimeSFromDateOnlyVals(2022, 6, 9),
            SredNarPlan = 4.1f,
            Status = 0,
            TrainingFrom = ZHelper.TimeSFromDateOnlyVals(2022, 01, 10),
            TrainingTo = ZHelper.TimeSFromDateOnlyVals(2022, 6, 5),
            WarningType = 1,
            WarningTime = ZHelper.TimeSFromDateTime(new DateTime(2022, 7, 5, 5, 11, 30)),
            WarningFrom = ZHelper.TimeSFromDateOnlyVals(2022, 7, 5),
            WarningSensor = 1,
            ErrorRate = 2
        }, gMetadata);
        ZHelper.Print(gobj1);

        Console.WriteLine("!!! update");
        var gobj2 = gobclient.Put(new GObject
        {
            Id = 26,
            Kod = 987,
            Name = "Объект 999",
            TekNar = 222,
            LastTo = ZHelper.TimeSFromDateOnlyVals(2021, 9, 10),
            ToTime = 305,
            ToNar = 3007,
            PlanYear = 6000,
            Koef2 = 0.4f,
            Koef1 = 1.02f,
            SredNar = 9.06f,
            DateFrom = ZHelper.TimeSFromDateOnlyVals(2022, 2, 12),
            NarFrom = 1,
            NextTo = ZHelper.TimeSFromDateOnlyVals(2022, 4, 9),
            SredNarPlan = 4.1f,
            Status = 0,
            TrainingFrom = ZHelper.TimeSFromDateOnlyVals(2022, 2, 10),
            TrainingTo = ZHelper.TimeSFromDateOnlyVals(2022, 6, 5),
            WarningType = 1,
            WarningTime = ZHelper.TimeSFromDateTime(new DateTime(2022, 7, 5, 5, 11, 30)),
            WarningFrom = ZHelper.TimeSFromDateOnlyVals(2022, 7, 5),
            WarningSensor = 1,
            ErrorRate = 2
        }, gMetadata);
        ZHelper.Print(gobj2);

        Console.WriteLine("!!! delete");
        var gobj3 = gobclient.Delete(new GObjectId { Id = gobj1.Id }, gMetadata);

    }

    private static GrpcChannel GetChannel()
    {

        IConfiguration conf = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile(@"appsettings.json")
                                 .Build();
        string strChannel = "";
        bool isLocal = bool.Parse(conf.GetSection("ChannelType:IsLocal").Value);
        if (isLocal)
            strChannel = conf.GetSection("Channels:Local").Value;
        else
            strChannel = conf.GetSection("Channels:Remote").Value;

        return GrpcChannel.ForAddress(strChannel);

    }

    private static Metadata? GetMetadata(string token)
    {
        var headers = new Metadata();
        headers.Add("Authorization", $"Bearer {token}");
        return headers;
    }

    private static string GetToken()
    {

        IConfiguration conf = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile(@"appsettings.json")
                                 .Build();
        return conf.GetSection("JWTBearer:Token").Value;
    }
    private static HttpClient? StrChanel()
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        return new HttpClient(httpClientHandler);
    }

    private static string WUploadFile()
    {
        IConfiguration conf = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile(@"appsettings.json")
                                  .Build();
        return conf.GetSection("Files:WitsmlUploadFile").Value;
    }
}