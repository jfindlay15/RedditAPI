namespace WebSockets.GeneratedModels
{
    public class Rootobject
    {
        public string kind { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string after { get; set; }
        public int dist { get; set; }
        public object modhash { get; set; }
        public string geo_filter { get; set; }
        public Child[] children { get; set; }
        public object before { get; set; }
    }

    public class Child
    {
        public string kind { get; set; }
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public string subreddit_id { get; set; }
        public object approved_at_utc { get; set; }
        public bool author_is_blocked { get; set; }
        public object comment_type { get; set; }
        public string link_title { get; set; }
        public object mod_reason_by { get; set; }
        public object banned_by { get; set; }
        public int ups { get; set; }
        public object num_reports { get; set; }
        public string author_flair_type { get; set; }
        public int total_awards_received { get; set; }
        public string subreddit { get; set; }
        public string link_author { get; set; }
        public object likes { get; set; }
        public string replies { get; set; }
        public object[] user_reports { get; set; }
        public bool saved { get; set; }
        public string id { get; set; }
        public object banned_at_utc { get; set; }
        public object mod_reason_title { get; set; }
        public int gilded { get; set; }
        public bool archived { get; set; }
        public object collapsed_reason_code { get; set; }
        public bool no_follow { get; set; }
        public string author { get; set; }
        public int num_comments { get; set; }
        public bool can_mod_post { get; set; }
        public bool send_replies { get; set; }
        public string parent_id { get; set; }
        public int score { get; set; }
        public string author_fullname { get; set; }
        public bool over_18 { get; set; }
        public object report_reasons { get; set; }
        public object removal_reason { get; set; }
        public object approved_by { get; set; }
        public int controversiality { get; set; }
        public string body { get; set; }
        public bool edited { get; set; }
        public object top_awarded_type { get; set; }
        public int downs { get; set; }
        public string author_flair_css_class { get; set; }
        public bool is_submitter { get; set; }
        public bool collapsed { get; set; }
        public Author_Flair_Richtext[] author_flair_richtext { get; set; }
        public bool author_patreon_flair { get; set; }
        public string body_html { get; set; }
        public Gildings gildings { get; set; }
        public object collapsed_reason { get; set; }
        public string distinguished { get; set; }
        public object associated_award { get; set; }
        public bool stickied { get; set; }
        public bool author_premium { get; set; }
        public bool can_gild { get; set; }
        public string link_id { get; set; }
        public object unrepliable_reason { get; set; }
        public string author_flair_text_color { get; set; }
        public bool score_hidden { get; set; }
        public string permalink { get; set; }
        public string subreddit_type { get; set; }
        public string link_permalink { get; set; }
        public string name { get; set; }
        public string author_flair_template_id { get; set; }
        public string subreddit_name_prefixed { get; set; }
        public string author_flair_text { get; set; }
        public object[] treatment_tags { get; set; }
        public float created { get; set; }
        public float created_utc { get; set; }
        public object[] awarders { get; set; }
        public object[] all_awardings { get; set; }
        public bool locked { get; set; }
        public string author_flair_background_color { get; set; }
        public object collapsed_because_crowd_control { get; set; }
        public object[] mod_reports { get; set; }
        public bool quarantine { get; set; }
        public object mod_note { get; set; }
        public string link_url { get; set; }
        public Media_Metadata media_metadata { get; set; }
    }

    public class Gildings
    {
    }

    public class Media_Metadata
    {
        public Ehfm5o80paqa1 ehfm5o80paqa1 { get; set; }
    }

    public class Ehfm5o80paqa1
    {
        public string status { get; set; }
        public string e { get; set; }
        public string m { get; set; }
        public P[] p { get; set; }
        public S s { get; set; }
        public string id { get; set; }
    }

    public class S
    {
        public int y { get; set; }
        public int x { get; set; }
        public string u { get; set; }
    }

    public class P
    {
        public int y { get; set; }
        public int x { get; set; }
        public string u { get; set; }
    }

    public class Author_Flair_Richtext
    {
        public string a { get; set; }
        public string e { get; set; }
        public string u { get; set; }
        public string t { get; set; }
    }
}

