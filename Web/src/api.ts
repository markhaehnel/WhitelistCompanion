export const getQueryParam = (param: string): string | null => {
    let params = new URL(document.location.toString()).searchParams;
    return params.get(param);
};

export const removeQueryParam = (param: string) => {
    let url = new URL(document.location.toString());
    url.searchParams.delete(param);

    history.pushState({}, "Whitelist Companion", url);
};

var href = new URL("https://google.com?q=cats");
href.searchParams.delete("q");
console.log(href.toString()); // https://google.com/?q=dogs

export const fetchWhitelist = async () => {
    const res = await fetch("/whitelist", {
        headers: {
            ApiKey: getQueryParam("secret") ?? "",
        },
        method: "GET",
        mode: "cors",
    });

    if (!res.ok) {
        const error = new Error(
            "An error occurred while fetching the whitelist."
        );
        throw error;
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
        const error = new Error(
            "An error occurred while adding to the whitelist."
        );
        throw error;
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
        const error = new Error(
            "An error occurred while fetching the userlist."
        );
        throw error;
    }

    return res.json();
};
