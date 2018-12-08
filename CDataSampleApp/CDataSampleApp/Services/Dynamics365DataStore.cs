using System;
using System.Collections.Generic;
using System.Data;
using System.Data.CData.DynamicsCRM;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDataSampleApp.Models;

namespace CDataSampleApp.Services
{
    class Dynamics365DataStore : IDataStore<Item>
    {
        List<Item> items;
        private string connectionString = "";

        public async Task<bool> AddItemAsync(Item item)
        {
            var sql = $"insert into Task(Subject,Description)values('{item.Text}','{item.Description}')";
            var result = ExecuteForDynamics365(sql);

            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var sql = $"Update set Subject = '{item.Text}', Description = '{item.Description}' where Id = '{item.Id}'";
            var result = ExecuteForDynamics365(sql);

            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var sql = $"Delete from Task where Id = '{id}'";
            var result = ExecuteForDynamics365(sql);

            return await Task.FromResult(result);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var resultItem = QueryForDynamics365ById(id);
            return await Task.FromResult(resultItem);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var resultItems = QueryForDynamics365();
            return await Task.FromResult(resultItems);
        }

        private bool ExecuteForDynamics365(string sql)
        {
            var response = 0;

            using (var connection = new DynamicsCRMConnection(connectionString))
            {
                var cmd = new DynamicsCRMCommand(sql, connection);
                response = cmd.ExecuteNonQuery();
            }

            return response == 0 ? false : true;
        }

        private Item QueryForDynamics365ById(string id)
        {
            return QueryForDynamics365(id).FirstOrDefault();
        }

        private List<Item> QueryForDynamics365()
        {
            return QueryForDynamics365(null);
        }

        private List<Item> QueryForDynamics365(string id)
        {
            List<Item> items = new List<Item>();

            using (var connection = new DynamicsCRMConnection(connectionString))
            {
                var sql = "SELECT Id, Subject, Description FROM Task";
                sql += id == null ? "" : $" where Id = '{id}'";

                var dataAdapter = new DynamicsCRMDataAdapter(sql, connection);
                var table = new DataTable();
                dataAdapter.Fill(table);

                foreach (DataRow row in table.Rows)
                    items.Add(new Item()
                    {
                        Id = row["Id"].ToString(),
                        Text = row["Subject"].ToString(),
                        Description = row["Description"].ToString()
                    });
            }

            return items;
        }


    }
}
