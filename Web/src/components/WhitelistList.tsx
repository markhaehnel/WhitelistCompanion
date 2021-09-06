import React, { useState } from "react";
import { useQueries, useQuery } from "react-query";
import { fetchWhitelist } from "../api";
import { Card } from "./Card";
import { Loader } from "./Loader";

export function WhitelistList() {
    const {
        data: users,
        error,
        isFetching,
    } = useQuery("whitelist", async () => {
        let data = await fetchWhitelist();
        return data?.data?.users.sort((a: string, b: string) =>
            a.toLowerCase().localeCompare(b.toLowerCase())
        );
    });

    return (
        <Card>
            <div className="">
                <div className="flex flex-row m-4">
                    <div className="text-xl">Whitelist</div>
                    <div className="flex-grow"></div>
                    {isFetching && <Loader />}
                </div>

                <div className="flex flex-col overflow-y-scroll h-[320px]">
                    {error && (
                        <div className="text-md py-2 px-4">
                            Error while fetching whitelist.
                        </div>
                    )}
                    {!error && users && (
                        <div className="flex flex-col">
                            {users.map((user: string) => {
                                return (
                                    <div
                                        className="text-md odd:bg-gray-100 py-2 px-4 hover:bg-gray-200"
                                        key={user}
                                    >
                                        {user}
                                    </div>
                                );
                            })}
                        </div>
                    )}
                </div>

                <div className="h-4"></div>
            </div>
        </Card>
    );
}
