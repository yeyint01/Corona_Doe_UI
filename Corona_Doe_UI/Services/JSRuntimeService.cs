using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace Corona_Doe_UI.Services
{
    public class JSRuntimeService
    {
        IJSRuntime _jsRuntime;
        public JSRuntimeService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task OpenModal(string omodal)
        {
            await _jsRuntime.InvokeVoidAsync(
                "CustomJsFunctions.openModal", omodal);
        }

        public async Task CloseModal(string omodal)
        {
            await _jsRuntime.InvokeVoidAsync(
            "CustomJsFunctions.closeModal", omodal);

            await _jsRuntime.InvokeVoidAsync(
           "CustomJsFunctions.checkModal");

        }

        public async Task Toast(string id, string text = "")
        {
            await _jsRuntime.InvokeVoidAsync("CustomJsFunctions.toast", id, text);
        }

        public async Task ToastArray(string id, List<String> list)
        {
            await _jsRuntime.InvokeVoidAsync("CustomJsFunctions.toastarray", id, list);
        }

        public async Task Focus(string inputid)
        {
            await _jsRuntime.InvokeVoidAsync("CustomJsFunctions.autofocus", inputid);
        }

        public async Task<string> TableWidth(string tableId)
        {
            var tablewidth = await _jsRuntime.InvokeAsync<int>("CustomJsFunctions.tableWidth", tableId);
            return (tablewidth.ToString());
        }

    }
}
