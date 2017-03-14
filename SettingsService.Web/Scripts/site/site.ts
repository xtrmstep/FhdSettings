/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

class ServiceApi {
    serviceUrl: string;

    setApiServer(baseServiceUrl: string) {
        this.serviceUrl = baseServiceUrl;
    }

    putAjax(url: string, jsonPayload: string, callback: () => any) {
        $.ajax({
            url: url,
            method: "PUT",
            data: jsonPayload,
            contentType: "application/json",
            success() { callback(); }
        });
    }

    getAjax(url: string, callback: (data: any) => any) {
        $.get(url, callback);
    }
}