﻿using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Features.Steps.Pages
{
    public class NewsPage : Page
    {
        public NewsPage(IWebDriver w) : base(w,"news")
        {

        }
    }
}
