using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.Server
{
    public static class RequestCalls
    {
        public const string users = "/users";
        public const string specific_user = "/users/";
        public const string sessions = "/sessions";
        public const string packages = "/packages";
        public const string transactions = "/transactions";
        public const string trans_packs = "/transactions/packages";
        public const string cards = "/cards";
        public const string deck = "/deck";
        public const string deck_plain = "/deck?format=plain";
        public const string stats = "/stats";
        public const string score = "/score";
        public const string battles = "/battles";
        public const string tradings = "/tradings";
    }
}
