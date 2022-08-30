namespace PostBin.Shared
{
    public static class StoredProcedures
    {
        public static readonly string get_posts = "sp_get_posts";
        public static readonly string get_post = "sp_get_post";
        public static readonly string create_post = "sp_create_post";
        public static readonly string update_post = "sp_update_post";
        public static readonly string delete_post = "sp_delete_post";

        public static readonly string get_user = "sp_get_user";
        public static readonly string get_user_by_username = "sp_get_user_by_username";
        public static readonly string create_user = "sp_create_user";
        public static readonly string update_user = "sp_update_user";

    }
}