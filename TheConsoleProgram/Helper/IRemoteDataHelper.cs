using System;
using Models;

namespace Helper
{
	public interface IRemoteDataHelper<T> where T : class
	{
        RemoteDataResponse<T> GetAndParseData(string url);
    }
}

