using System;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Corona_Doe_UI.Services
{
    public class BrowserResize
    {
        public static event Func<Task> OnResize;
        public static event Func<Task> OnMenuToggle;
        private IJSRuntime _jsRuntime;

        public BrowserResize(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        [JSInvokable]
        public static async Task OnBrowserResize()
        {
            if (OnResize == null)
                return;

            await OnResize.Invoke();
        }

        public async Task<(int, int)> GetResize()
        {
            var data = await _jsRuntime.InvokeAsync<object>("CustomJsFunctions.getWindowScreen");
            var obj = JObject.Parse(data.ToString());

            return ((int)obj["width"], (int)obj["height"]);
        }

        [JSInvokable]
        public static void OnNavMenuToggle()
        {
            if (OnMenuToggle == null)
                return;

            OnMenuToggle.Invoke();
        }
    }
}
