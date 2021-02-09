namespace kvin {
    export class utilities {
        alertTemplate: string = '<div class="alert alert-warning alert-dismissible fade show col-md-6 mx-auto" role="alert">Short link: <strong><a id="redirectUrl" href={redirectUrl}>{redirectUrl}</a></strong><button type="button" class="btn" aria-label="Copy" onclick="kvin.utilities.CopyUrl(\'{redirectUrl}\')">Copy</button><button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button></div>';

        AddUrl(url:string, name:string) {
            fetch('/',
            {
                method: 'POST',
                body: JSON.stringify({
                    "name": name,
                    "url": url
                }),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                //This wonky syntax performs a full replace on the string vs string.replace in js which only replaces the first occurence.
                let alert:string = this.alertTemplate.split("{redirectUrl}").join(data.redirectUrl);
                let element = document.getElementById("Feedback");
                if(element != null)
                    element.innerHTML = alert;
            });
        }

        
    }

    export class alert
    {
        static async CopyUrl(redirectUrl:string)
        {
            await navigator.clipboard.writeText(redirectUrl);
        }
    }
}


