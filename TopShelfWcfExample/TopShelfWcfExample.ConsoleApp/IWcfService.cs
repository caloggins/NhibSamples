﻿namespace TopShelfWcfExample.ConsoleApp
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        string Greet();
    }
}