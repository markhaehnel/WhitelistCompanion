export const getSecretFromLocation = (): string => {
    let params = new URL(document.location.toString()).searchParams;
    return params.get("secret") ?? "";
};

export const fetchWhitelist = async () => {
    const res = await fetch("/whitelist", {
        headers: {
            ApiKey: getSecretFromLocation(),
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
            ApiKey: getSecretFromLocation(),
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
            ApiKey: getSecretFromLocation(),
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
