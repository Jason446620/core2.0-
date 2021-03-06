﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coreModel
{
    public class BaseDB
    {
        public static SqlSugarClient GetClient() {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = BaseDBConfig.ConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection=true
            });
            //后期改成日志
            db.Aop.OnLogExecuting = (sql, pars) => {
                Console.WriteLine(sql+"\r\n"+db.Utilities.SerializeObject(pars.ToDictionary(it=>it.ParameterName,it=>it.Value)));
                Console.WriteLine();
            };
            return db;
        }
    }
}
