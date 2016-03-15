using LoklakDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LoklakWindowsDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        Loklak loklak = new Loklak("http://localhost:9000/api/");
        int i = 0;
        private DispatcherTimer dispatcherTimer;
        private RootObject result;

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var st = new LoklakSearchTerm();
            st.terms = "fossasia";
            var resultstr = await loklak.search(st);
            result = JsonConvert.DeserializeObject<RootObject>(resultstr);
            stp.DataContext = result.statuses[i];
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            if(i==result.statuses.Count-1)
            {
                i = -1;
            }
            stp.DataContext = result.statuses[++i];
        }
    }

    public class SearchMetadata
    {
        public string itemsPerPage { get; set; }
        public string count { get; set; }
        public int count_twitter_all { get; set; }
        public int count_twitter_new { get; set; }
        public int count_backend { get; set; }
        public int count_cache { get; set; }
        public int hits { get; set; }
        public int period { get; set; }
        public string query { get; set; }
        public string client { get; set; }
        public int time { get; set; }
        public string servicereduction { get; set; }
        public string scraperInfo { get; set; }
    }

    public class User
    {
        public string screen_name { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
        public string profile_image_url_https { get; set; }
        public string appearance_first { get; set; }
        public string appearance_latest { get; set; }
    }

    public class Status
    {
        public string timestamp { get; set; }
        public string created_at { get; set; }
        public string screen_name { get; set; }
        public string text { get; set; }
        public string link { get; set; }
        public string id_str { get; set; }
        public string canonical_id { get; set; }
        public string parent { get; set; }
        public string source_type { get; set; }
        public string provider_type { get; set; }
        public int retweet_count { get; set; }
        public int favourites_count { get; set; }
        public List<object> images { get; set; }
        public int images_count { get; set; }
        public List<object> audio { get; set; }
        public int audio_count { get; set; }
        public List<object> videos { get; set; }
        public int videos_count { get; set; }
        public string place_name { get; set; }
        public string place_id { get; set; }
        public string place_context { get; set; }
        public string place_country { get; set; }
        public string place_country_code { get; set; }
        public List<double> place_country_center { get; set; }
        public List<double> location_point { get; set; }
        public int location_radius { get; set; }
        public List<double> location_mark { get; set; }
        public string location_source { get; set; }
        public List<object> hosts { get; set; }
        public int hosts_count { get; set; }
        public List<object> links { get; set; }
        public int links_count { get; set; }
        public List<object> mentions { get; set; }
        public int mentions_count { get; set; }
        public List<object> hashtags { get; set; }
        public int hashtags_count { get; set; }
        public string classifier_language { get; set; }
        public double classifier_language_probability { get; set; }
        public string classifier_emotion { get; set; }
        public double classifier_emotion_probability { get; set; }
        public int without_l_len { get; set; }
        public int without_lu_len { get; set; }
        public int without_luh_len { get; set; }
        public User user { get; set; }
    }

    public class Aggregations
    {
    }

    public class RootObject
    {
        public string readme_0 { get; set; }
        public string readme_1 { get; set; }
        public string readme_2 { get; set; }
        public string readme_3 { get; set; }
        public SearchMetadata search_metadata { get; set; }
        public List<Status> statuses { get; set; }
        public Aggregations aggregations { get; set; }
    }
}
