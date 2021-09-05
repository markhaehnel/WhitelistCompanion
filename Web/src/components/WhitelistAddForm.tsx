import React, { useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { postWhitelist } from "../api";

import { Card } from "./Card";

export function WhitelistAddForm() {
    const queryClient = useQueryClient();
    const mutation = useMutation(postWhitelist);
    const [user, setUser] = useState("");

    return (
        <Card>
            <div className="flex flex-col gap-4 p-4">
                <input
                    tabIndex={10}
                    value={user}
                    onChange={(e) =>
                        setUser((e.target as HTMLInputElement).value)
                    }
                    type="text"
                    placeholder="Nickname"
                    disabled={mutation.isLoading}
                    className="text-lg p-2 rounded-md outline-none bg-gray-100 border-gray-300 border-2 transition-all focus:border-gray-600 focus:shadow-lg disabled:text-gray-600"
                />
                <input
                    type="submit"
                    value="Zur Whitelist hinzufügen"
                    onClick={async () => {
                        await mutation.mutateAsync(user);
                        queryClient.invalidateQueries();
                        setUser("");
                    }}
                    tabIndex={11}
                    disabled={mutation.isLoading}
                    className="w-full text-center text-lg text-white bg-blue-600 border-2 border-transparent p-2 rounded-md shadow cursor-pointer hover:bg-blue-500 transition-transform active:bg-blue-600 active:scale-95 focus:border-gray-600 disabled:bg-blue-300 disabled:cursor-not-allowed"
                />
            </div>
        </Card>
    );
}
