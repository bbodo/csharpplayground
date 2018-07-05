// Add the following using directives, and add a reference for System.Net.Http.  
using System.Net.Http;
using System.IO;
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AsyncExampleWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            resultsTextBox.Clear();
        }

        int ClickCount = 0;

        // does not even return a task because it is simply an event handler
        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            //resultsTextBox.Clear();
            ClickCount += 1;
            startButton.IsEnabled = false;

            if(ClickCount == 1)
            {
                resultsTextBox.Text += "\n===================== With synchronous processing: \n note the frozen, uncloseable window\n";
                SumPageSizes();
            }

            if(ClickCount == 2)
            {
                resultsTextBox.Text += "\n===================== With sequential asynchronous processing\n";
                // Two-step async call.
                Task Task1 = SumPageSizesAsync_Standard();
                await Task1;
            }


            if(ClickCount == 3)
            {
                resultsTextBox.Text += "\n===================== With Task.WhenAll() / parallel processing";
                // One-step async call.  
                await SumPageSizesAsync_Parallel();
            }
            resultsTextBox.Text += "\r\nControl returned to startButton_Click.\r\n";
            startButton.IsEnabled = true;
        }


        private async Task SumPageSizesAsync_Standard()
        {
            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            // With Task.WhenAll(), this loop will be replaced with a function and Task.WhenAll, see below
            var total = 0;
            foreach (var url in urlList)
            {
                byte[] urlContents = await GetURLContentsAsync(url);

                // The previous line abbreviates the following two assignment statements.  
                // GetURLContentsAsync returns a Task<T>. At completion, the task  
                // produces a byte array.  
                //Task<byte[]> getContentsTask = GetURLContentsAsync(url);  
                //byte[] urlContents = await getContentsTask;  

                DisplayResults(url, urlContents);

                // Update the total.            
                total += urlContents.Length;
            }

            // Display the total count for all of the websites.  
            resultsTextBox.Text +=
                string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }

        private async Task SumPageSizesAsync_Parallel()
        {
            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            // Create a query.   
            // HERE, THE LOOP WAS REPLACED WITH FUNCTION, AND Task.WhenAll below
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURLAsync(url);

            // Use ToArray to execute the query and start the download tasks.  
            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();

            // You can do other work here before awaiting.  

            // Await the completion of all the running tasks.  
            int[] lengths = await Task.WhenAll(downloadTasks);

            //// The previous line is equivalent to the following two statements.  
            //Task<int[]> whenAllTask = Task.WhenAll(downloadTasks);  
            //int[] lengths = await whenAllTask;  

            int total = lengths.Sum();

            // Display the total count for all of the websites.  
            resultsTextBox.Text +=
                string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }
        // The actions from the foreach loop are moved to this async method.  
        private async Task<int> ProcessURLAsync(string url)
        {
            var byteArray = await GetURLContentsAsync(url);
            DisplayResults(url, byteArray);
            return byteArray.Length;
        }

        private async Task<byte[]> GetURLContentsAsync(string url)
        {
            // The downloaded resource ends up in the variable named content.  
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.  
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for  
            // the response.  
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                // Get the data stream that is associated with the specified url.  
                using (Stream responseStream = response.GetResponseStream())
                {
                    await responseStream.CopyToAsync(content);
                }
            }

            // Return the result as a byte array.  
            return content.ToArray();

        }

        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com/library/hh290136.aspx",
                "http://msdn.microsoft.com/library/ee256749.aspx",
                "http://msdn.microsoft.com/library/hh290138.aspx",
                "http://msdn.microsoft.com/library/hh290140.aspx",
                "http://msdn.microsoft.com/library/dd470362.aspx",
                "http://msdn.microsoft.com/library/aa578028.aspx",
                "http://msdn.microsoft.com/library/ms404677.aspx",
                "http://msdn.microsoft.com/library/ff730837.aspx",
                "http://descil.ethz.ch",
                "https://art.marameier.ch"
            };
            return urls;
        }

        private void DisplayResults(string url, byte[] content)
        {
            // Display the length of each website. The string format   
            // is designed to be used with a monospaced font, such as  
            // Lucida Console or Global Monospace.  
            var bytes = content.Length;
            // Strip off the "http://".  
            var displayURL = url.Replace("http://", "");
            resultsTextBox.Text += string.Format("\n{0,-58} {1,8}", displayURL, bytes);

        }


        private void SumPageSizes()
        {
            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            var total = 0;
            foreach (var url in urlList)
            {
                // GetURLContents returns the contents of url as a byte array.  
                byte[] urlContents = GetURLContents(url);

                DisplayResults(url, urlContents);

                // Update the total.  
                total += urlContents.Length;
            }

            // Display the total count for all of the web addresses.  
            resultsTextBox.Text +=
                string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }
        private byte[] GetURLContents(string url)
        {
            // The downloaded resource ends up in the variable named content.  
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.  
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for  
            // the response.  
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.  
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL.  
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.    
                    responseStream.CopyTo(content);
                }
            }

            // Return the result as a byte array.  
            return content.ToArray();
        }
    }
}