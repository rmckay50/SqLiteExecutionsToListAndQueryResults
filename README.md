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
