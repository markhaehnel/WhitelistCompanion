import React from "react";
import { useQuery } from "react-query";
import { fetchWhitelist } from "../api";
import { Card } from "./Card";
import { Loader } from "./Loader";

export function WhitelistList() {
    const { data, error, isFetching } = useQuery("whitelist", fetchWhitelist);

    return (
        <Card>
            <div>
                <div className="flex flex-row m-4">
                    <div className="text-xl">Whitelist</div>
                    <div className="flex-grow"></div>
                    {isFetching && <Loader />}
                </div>

                <div className="flex flex-col">
                    {error && (
                        <div className="text-md py-2 px-4">
                            Error while fetching whitelist.
                        </div>
                    )}

                    {!error &&
                        data &&
                        data?.data?.users?.map((user: string) => {
                            return (
                                <div
                                    className="text-md odd:bg-gray-100 py-2 px-4"
                                    key={user}
                                >
                                    {user}
                                </div>
                            );
                        })}
                </div>

                <div className="h-4"></div>
            </div>
        </Card>
    );
}
