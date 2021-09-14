export const getQueryParam = (param: string): string | null => {
    let params = new URL(document.location.toString()).searchParams;
    return params.get(param);
};

export const removeQueryParam = (param: string) => {
    let url = new URL(document.location.toString());
    url.searchParams.delete(param);

    history.pushState({}, "Whitelist Companion", url);
};

export const fetchWhitelist = async () => {
    const res = await fetch("/whitelist", {
        headers: {
            ApiKey: getQueryParam("secret") ?? "",
        },
        method: "GET",
        mode: "cors",
    });

    if (!res.ok) {
        throw new HttpError(res.status, res.statusText);
    }

    return res.json();
};

export const postWhitelist = async (user: string) => {
    const res = await fetch("/whitelist", {
        headers: {
            ApiKey: getQueryParam("secret") ?? "",
            "Content-Type": "application/json",
        },
        method: "POST",
        mode: "cors",
        body: JSON.stringify({ user }),
    });

    if (!res.ok) {
        throw new HttpError(res.status, res.statusText);
    }

    return res.json();
};

export const fetchUserList = async () => {
    const res = await fetch("/userlist", {
        headers: {
            ApiKey: getQueryParam("secret") ?? "",
        },
        method: "GET",
        mode: "cors",
    });

    if (!res.ok) {
        throw new HttpError(res.status, res.statusText);
    }

    return res.json();
};

export class HttpError extends Error {
    constructor(statusCode: number, msg: string) {
        super(msg);
        Object.setPrototypeOf(this, HttpError.prototype);

        this.statusCode = statusCode;
    }

    public readonly statusCode: number;
}
