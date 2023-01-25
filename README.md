# SqLiteExecutionsToListAndQueryResults

2023 01 22 0810  
*	commit - ported from SqLiteExecutionsToListAndQueryResults 1.linq  
    opened C:\data\NinjaTrader.sqlite  
    selected all from executins  
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

