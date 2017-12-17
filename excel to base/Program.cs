using System;
using Excel = ExcelDataReader;
using Http = System.Net.Http;
using System.Text;
using System.Web;

namespace excel_to_base
{
    class Program
    {
        static void Main(string[] args)
        {
            


            String link = " http://www.cbr.ru/scripts/XML_daily.asp";
            saveXmlHttpResponseToXmlFile(link);
            Console.ReadLine();
        }
        
        static async void saveXmlHttpResponseToXmlFile(String url)
        {
            //get Http response from < String url>
            //
            Http.HttpClient httpClient = new Http.HttpClient();
            try
            {
                Http.HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Byte[] content_in_bytes = await response.Content.ReadAsByteArrayAsync();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding e = Encoding.GetEncoding(1251);
                Decoder d = e.GetDecoder();
                int char_count = d.GetCharCount(content_in_bytes, 0, content_in_bytes.Length);
                Char[] content_in_chars = new Char[char_count];
                int chars_decoded_count =  d.GetChars(content_in_bytes,0, content_in_bytes.Length, content_in_chars, char_count, false);

                Console.WriteLine("{0}", content_in_chars);
            }
            catch (Http.HttpRequestException e)
            {
                Console.WriteLine("{0}", e);
            }
        }

        static void showEncodings() {
            // Print the header.
            Console.Write("CodePage identifier and name     ");
            Console.Write("BrDisp   BrSave   ");
            Console.Write("MNDisp   MNSave   ");
            Console.WriteLine("1-Byte   ReadOnly ");

            // For every encoding, get the property values.
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();

                Console.Write("{0,-6} {1,-25} ", ei.CodePage, ei.Name);
                Console.Write("{0,-8} {1,-8} ", e.IsBrowserDisplay, e.IsBrowserSave);
                Console.Write("{0,-8} {1,-8} ", e.IsMailNewsDisplay, e.IsMailNewsSave);
                Console.WriteLine("{0,-8} {1,-8} ", e.IsSingleByte, e.IsReadOnly);
            }
        }
    }


}
