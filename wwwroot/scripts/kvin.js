"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var kvin;
(function (kvin) {
    class utilities {
        constructor() {
            this.alertTemplate = '<div class="alert alert-warning alert-dismissible fade show col-md-6 mx-auto" role="alert">Short link: <strong><a id="redirectUrl" href={redirectUrl}>{redirectUrl}</a></strong><button type="button" class="btn" aria-label="Copy" onclick="kvin.utilities.CopyUrl(\'{redirectUrl}\')">Copy</button><button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button></div>';
        }
        AddUrl(url, name) {
            fetch('/', {
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
                let alert = this.alertTemplate.split("{redirectUrl}").join(data.redirectUrl);
                let element = document.getElementById("Feedback");
                if (element != null)
                    element.innerHTML = alert;
            });
        }
    }
    kvin.utilities = utilities;
    class alert {
        static CopyUrl(redirectUrl) {
            return __awaiter(this, void 0, void 0, function* () {
                yield navigator.clipboard.writeText(redirectUrl);
            });
        }
    }
    kvin.alert = alert;
})(kvin || (kvin = {}));
//# sourceMappingURL=kvin.js.map