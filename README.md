# SqLiteExecutionsToListAndQueryResults

2023 01 22 0810  
*	commit - ported from SqLiteExecutionsToListAndQueryResults 1.linq  
    opened C:\data\NinjaTrader.sqlite  
    selected all from executions  
    used SQLiteDataReader to get data from executions  
    created 'List<Executions> listExecution' to store input  
    requires createing a new Executions object and adding to listExecution for each line SQLiteDataReader reads  
    each item (exec.Id) requires a cast because of sqlite's typing system  
    double is a problem - it's real in sqlite  
    using (System.Double) works in LinqPad  
    will see if it works in VS and then NT  
    it worked  

2023 01 22 0850  
*   commit - Mixed mode assembly is built against version ‘v2.0.50727′ of the runtime  
    got this error on run  
    https://stackoverflow.com/questions/6425707/mixed-mode-assembly-is-built-against-version-v2-0-50727-of-the-runtime  
    replace app.config <startup></startup> with:  
```
<startup useLegacyV2RuntimeActivationPolicy="true">
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
    <requiredRuntime version="v4.0.20506" />
  </startup>
```

2023 01 22 1145  
*   commit - adding Executions columns through StatementDate  

2023 01 24 1042 1135  
*   commit - finished adding properties to Executions class  
    and now start fucking error:  
    'The type or namespace name 'ExecutionsClass' could not be found (are you missing a using directive or an assembly reference?)'  

2023 01 25 1200  
*   commit - error 'namespace name 'ExecutionsClass' could not be found ' is from not adding 
    file to project I think  

2023 01 25 1243  
*   commit - retrieves data from .sqlite db and creates list that will repalce instList(Ret)  
    sometimes the query list is 0   

2023 01 25 1243  
*   commit - sometimes the result is zero  
    don't know why  
    TODO  
    get Ret class properties  

2023 01 25 1955  
*   commit - where is Ret() filled in creating routines?  
    'Extension to Create List from NT Db for 2nd Version Fill fnList Prices (conflicted).linq'  

2023 01 26 0425  
*   commit - changed Ret.IsEntry and Ret.IsExit to bool? from long? in Extensions  
    will be easier to understand instList list  

2023 01 26 1440  
*   commit - queried list in Ret() format using 'instrument'  
    did not fill in expiry - it is located in 'Instruments'  

2023 01 26 1500  
*   commit - it works - creates the equivalent of instlist  
    TODO
    transfer to an indicator in NT  

2023 01 30 0650  
*   commit - 'SQLiteForNinjaTraderSQLiteDb.cs' in NinjaTrader.Custom is up to date on creating instList  
    TODO  
    locate section in 'CreateExtensionGetInsList.cs' where instList is created  
    it's 'getInstList.cs'  it returns instList  

2023 02 04 1605  
*   commit - adding 'Create workingTrades'  

2023 02 04 1610  
*   commit - create .dll to replace getInstList  

2023 02 05 0210  
*   commit - all coded from Program.cs (call NinjaTrader.sqlite, create, and return instList has been transferred to 'getInstList')  

2023 02 05 0215  
*   commit - added new project 'getInstListSqLite' to hold getInstListSqLite.cs and create .dll  

2023 02 05 0330  
*   commit - GetInstListSqLite.cs changed to project and 'getInstListSqLite.dll' - namespace - GetInstListSqLite  
    had problem with 'cannot convert List<Ret.Ret> to List<Ret.Ret>  
    solved problem using var when creating 'instList'  
    reason is that the namespaces are different    

2023 02 05 1310  
*   commit - adding Creat workingTrades

2023 02 05 1535  
*   commit - cannot convert from int? to int solved  
    in 'ExtensionGetActiveEntry' kept getting cannot comvert int? to int  
    problem was index on source.Trades[xx] needs to be int not int?  
    I had failsafed some of the properties in int?/long  
    VS doesn't think through the problem and a cast won't work  

2023 02 05 1605  
*   commit -  build succeded but skeptical  

2023 02 05 1620  
*   commit - compiling up to 'source.Fill();'  

2023 02 05 1735  
*   commit - added 'FillLongShortColumnInTradesList'  

2023 02 05 1750  
*   commit - added 'FillProfitLossColumnInTradesList'

2023 02 06 0205  
*   commit - added name to source.NTDrawLine to make it easier to determine .csv file contents  