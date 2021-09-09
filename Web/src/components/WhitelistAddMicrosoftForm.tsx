import React, { useState } from "react";
import { getQueryParam } from "../api";
import { Loader } from "./Loader";

export function WhitelistAddMicrosoftForm() {
    const [msButtonDisabled, setMsButtonDisabled] = useState(false);

    return (
        <div className="flex flex-col p-4">
            <button
                onClick={() => {
                    setMsButtonDisabled(true);
                    window.location.href = `/auth?ApiKey=${getQueryParam(
                        "secret"
                    )}&state=${getQueryParam("secret")}`;
                }}
                disabled={msButtonDisabled}
                className="w-full text-center text-lg text-white bg-black border-2 border-transparent p-2 rounded-md shadow cursor-pointer hover:bg-blue-500 hover:shadow-lg active:bg-blue-600 active:scale-95 focus:border-gray-600 disabled:bg-blue-300 dark:disabled:bg-gray-700 dark:disabled:text-gray-400 disabled:cursor-not-allowed transition-all"
            >
                {msButtonDisabled ? (
                    <div className="flex flex-row items-center justify-center gap-2">
                        <div className="inline-flex">
                            <Loader />
                        </div>{" "}
                        Verarbeite...
                    </div>
                ) : (
                    <>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 23 23"
                            className="inline-block w-6 h-6 -ml-1 mr-3 -mt-2"
                        >
                            <path fill="#f35325" d="M1 1h10v10H1z" />
                            <path fill="#81bc06" d="M12 1h10v10H12z" />
                            <path fill="#05a6f0" d="M1 12h10v10H1z" />
                            <path fill="#ffba08" d="M12 12h10v10H12z" />
                        </svg>
                        Mit Microsoft Account hinzufügen
                    </>
                )}
            </button>
        </div>
    );
}
