mergeInto(LibraryManager.library, {
    OverrideFetch: function(serverUrl) {
        var originalFetch = fetch;
        fetch = function(url, data) {
            console.log("modified fetch called: " + url);
            if(data != null)
            {
                var modifiedData = Object.assign(data, {credentials: "include"});
                return originalFetch(url, modifiedData);
            }
            return originalFetch(url, {credentials: "include"});
        };
    }
});