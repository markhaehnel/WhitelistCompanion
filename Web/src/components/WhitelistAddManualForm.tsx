import React, { useEffect, useRef, useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { getQueryParam, postWhitelist, removeQueryParam } from "../api";

import { Loader } from "./Loader";

export function WhitelistAddManualForm() {
    const queryClient = useQueryClient();
    const mutation = useMutation(postWhitelist);
    const [user, setUser] = useState("");
    const [userAdded, setUserAdded] = useState(false);
    const [userAddedError, setUserAddedError] = useState("");
    const inputRef = useRef<HTMLInputElement>(null);

    async function addToWhitelist(user: string) {
        try {
            await mutation.mutateAsync(user);
            queryClient.invalidateQueries();
            setUser("");
            setUserAdded(true);
            inputRef?.current?.focus();
        } catch {
            setUserAdded(false);
            setUserAddedError(
                "Ungültiger Username oder bereits auf der Whitelist"
            );
            setTimeout(() => setUserAddedError(""), 5000);
        }
    }

    useEffect(() => {
        inputRef?.current?.focus();
    });

    const userToAdd = getQueryParam("addUser");
    if (userToAdd) {
        removeQueryParam("addUser");
        addToWhitelist(userToAdd);
    }

    return (
        <div className="flex flex-col gap-4 p-4">
            <input
                value={user}
                onChange={(e) => setUser((e.target as HTMLInputElement).value)}
                onKeyDown={(e) => {
                    if (e.key === "Enter") {
                        addToWhitelist(user);
                    }
                }}
                type="text"
                placeholder="Nickname"
                ref={inputRef}
                disabled={mutation.isLoading}
                className="text-lg p-2 rounded-md outline-none bg-gray-100 border-gray-300 dark:bg-gray-600 dark:border-transparent dark:placeholder-gray-400 border-2 transition-all focus:border-gray-600 focus:shadow-lg disabled:text-gray-400 dark:disabled:text-gray-500"
            />
            <button
                onClick={() => addToWhitelist(user)}
                disabled={
                    mutation.isLoading || user === "" || userAddedError !== ""
                }
                className={`w-full text-center text-lg text-white bg-blue-600 border-2 border-transparent p-2 rounded-md shadow cursor-pointer hover:bg-blue-500 hover:shadow-lg active:bg-blue-600 active:scale-95 focus:border-gray-600 ${
                    userAddedError
                        ? "disabled:bg-red-700"
                        : "disabled:bg-blue-700"
                } ${
                    userAddedError
                        ? "dark:disabled:bg-red-700"
                        : "dark:disabled:bg-gray-700"
                } ${
                    userAddedError
                        ? "dark:disabled:text-white"
                        : "dark:disabled:text-gray-400"
                } disabled:cursor-not-allowed transition-all`}
            >
                {mutation.isLoading ? (
                    <div className="flex flex-row items-center justify-center gap-2">
                        <div className="inline-flex">
                            <Loader />
                        </div>{" "}
                        Verarbeite...
                    </div>
                ) : !userAddedError ? (
                    "Zur Whitelist hinzufügen"
                ) : (
                    userAddedError
                )}
            </button>
        </div>
    );
}
