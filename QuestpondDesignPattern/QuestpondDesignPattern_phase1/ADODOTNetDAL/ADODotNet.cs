﻿using CommonDAL;
using FactoryCustomer;
using InterfaceCustomer;
using InterfaceDal;
using Microsoft.Data.SqlClient;
namespace ADODOTNetDAL
{
    //Design Pattern:- Template Pattern
    public abstract class TemplateADO<AnyType> : AbstractDAL<AnyType> //Template
    {
        protected SqlConnection objConn = null;
        protected SqlCommand objComm = null;
        public TemplateADO(string connectionstring) : base(connectionstring)
        {
            
        }
        private void OpenConnection()
        {
            objConn=new SqlConnection(ConnectionString);
            objConn.Open();
            objComm = new SqlCommand();
            objComm.Connection = objConn;
        }

        protected abstract void ExecuteCommand(AnyType obj); // we want child classes to define this method

        protected abstract List<AnyType> ExecuteCommand(); // for search

        private void CloseConnection()
        {
            objConn.Close();
        }
        public void Execute(AnyType obj) // template method:  Fixed Sequence Insert
        {
            //fixed sequence
            OpenConnection();
            ExecuteCommand(obj);
            CloseConnection();

        }

        //for search as it returns list
        public List<AnyType> Execute() // Fixed Sequence select
        {
            //fixed sequence
            OpenConnection();
            List<AnyType> objTypes = ExecuteCommand();
            CloseConnection();
            return objTypes;

        }

        public override void Save()
        {
            foreach(AnyType o in AnyTypes)
            {
                Execute(o);
            }
        }

        public override List<AnyType> Search()
        {
            return Execute();
        }
    }

    public class CustomerDAL : TemplateADO<BaseCustomer>
    {
        public CustomerDAL(string _ConnectionString)
           : base(_ConnectionString)
        {

        }


        protected override void ExecuteCommand(BaseCustomer obj)
        {
            objComm.CommandText = "insert into tblCustomer(" +
                                           "CustomerName," +
                                           "BillAmount,"+
                                            "BillDate," +
                                           "PhoneNumber," +
                                           "CustomerAddress,CustomerType)" +
                                           "values('" + obj.CustomerName + "'," +
                                           obj.BillAmount + ",'" +
                                           obj.BillDate + "','" +
                                           obj.PhoneNumber + "','" +
                                           obj.CustomerAddress + "','" + obj.CustomerType + "')";
            objComm.ExecuteNonQuery();
        }

        protected override List<BaseCustomer> ExecuteCommand()
        {
            objComm.CommandText = "select * from tblCustomer";
            SqlDataReader dr = null;
            dr = objComm.ExecuteReader();
            List<BaseCustomer> custs = new List<BaseCustomer>();
            while (dr.Read())
            {
                BaseCustomer icust = FactoryUsingUnity<BaseCustomer>.CreateObject("Customer");
                icust.Id = Convert.ToInt32(dr["Id"]);
                icust.CustomerType = dr["CustomerType"].ToString();
                icust.CustomerName = dr["CustomerName"].ToString();
                icust.BillDate = Convert.ToDateTime(dr["BillDate"]);
                icust.BillAmount = Convert.ToDecimal(dr["BillAmount"]);
                icust.PhoneNumber = dr["PhoneNumber"].ToString();
                icust.CustomerAddress = dr["CustomerAddress"].ToString();
                custs.Add(icust);
            }
            return custs;
        }
    }
}