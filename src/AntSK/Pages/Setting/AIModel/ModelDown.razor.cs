﻿using AntDesign;
using AntSK.Domain.Model.hfmirror;
using AntSK.Models;
using AntSK.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RestSharp;

namespace AntSK.Pages.Setting.AIModel
{
    public partial class ModelDown
    {
        private readonly ListFormModel _model = new ListFormModel();
        private readonly IList<string> _selectCategories = new List<string>();

        private List<HfModels> _modelList = new List<HfModels>();



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            InitData("");
        }

        private void InitData(string searchKey) {
            var param = searchKey.Split(" ");

            string urlBase = "https://hf-mirror.com/models-json?sort=trending&search=gguf";
            if (param.Count() > 0)
            {
                urlBase+="+"+string.Join("+",param);
            }
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(urlBase, Method.Get);
            var response = client.Execute(request);
            var model = JsonConvert.DeserializeObject<HfModel>(response.Content);
            _modelList=model.models;
        }

        private async Task Search(string searchKey)
        {
            InitData(searchKey);
        }
    }
}
