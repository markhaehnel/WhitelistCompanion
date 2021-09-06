import React, { useEffect, useRef, useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { postWhitelist } from "../api";

import { Card } from "./Card";
import { Loader } from "./Loader";

export function WhitelistAddForm() {
    const queryClient = useQueryClient();
    const mutation = useMutation(postWhitelist);
    const [user, setUser] = useState("");
    const inputRef = useRef<HTMLInputElement>(null);

    async function addToWhitelist() {
        await mutation.mutateAsync(user);
        queryClient.invalidateQueries();
        setUser("");
        inputRef?.current?.focus();
    }

    useEffect(() => {
        inputRef?.current?.focus();
    });

    return (
        <Card>
            <div className="flex flex-col gap-4 p-4">
                <input
                    value={user}
                    onChange={(e) =>
                        setUser((e.target as HTMLInputElement).value)
                    }
                    onKeyDown={(e) => {
                        if (e.key === "Enter") {
                            addToWhitelist();
                        }
                    }}
                    type="text"
                    placeholder="Nickname"
                    ref={inputRef}
                    disabled={mutation.isLoading}
                    autoFocus
                    className="text-lg p-2 rounded-md outline-none bg-gray-100 border-gray-300 dark:bg-gray-600 dark:border-transparent dark:placeholder-gray-400 border-2 transition-all focus:border-gray-600 focus:shadow-lg disabled:text-gray-400 dark:disabled:text-gray-500"
                />
                <button
                    onClick={addToWhitelist}
                    disabled={mutation.isLoading || user === ""}
                    className="w-full text-center text-lg text-white bg-blue-600 border-2 border-transparent p-2 rounded-md shadow cursor-pointer hover:bg-blue-500 hover:shadow-lg active:bg-blue-600 active:scale-95 focus:border-gray-600 disabled:bg-blue-300 dark:disabled:bg-gray-700 dark:disabled:text-gray-400 disabled:cursor-not-allowed transition-all"
                >
                    {mutation.isLoading ? (
                        <div className="flex flex-row items-center justify-center gap-2">
                            <div className="inline-flex">
                                <Loader />
                            </div>{" "}
                            Verarbeite...
                        </div>
                    ) : (
                        "Zur Whitelist hinzufügen"
                    )}
                </button>
            </div>
        </Card>
    );
}
