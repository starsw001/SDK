# SDK聚合宝

##### **概论**
	Anime.SDK 是动漫的SDK
    
    sample:
    
   ``` c#
      //初始化
            var AnimeInit = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Init,
                    Proxy = new AnimeProxy
                    {
                        IP = "203.74.120.79",
                        Port = 3128
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeInit.ToJson());
            Thread.Sleep(1000);
            //搜索
            var AnimeSearch = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Search,
                    Proxy = new AnimeProxy(),
                    Search = new AnimeSearch
                    {
                        AnimeSearchKeyWord = "盾之勇者"
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeSearch.ToJson());
            Thread.Sleep(1000);
     

