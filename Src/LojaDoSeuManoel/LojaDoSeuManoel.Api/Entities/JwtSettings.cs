﻿namespace LojaDoSeuManoel.Api.Entities
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpireHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
