namespace PostBin.Shared
{
    public class AppSettings
    {
        #region ConnectionStrings
        public string DefaultDb { get; set; }
        #endregion
        
        #region Jwt
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpireTime { get; set; }
        #endregion
    }
}
