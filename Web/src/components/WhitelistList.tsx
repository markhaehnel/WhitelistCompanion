import React from "react";
import { useQuery } from "react-query";
import { fetchWhitelist } from "../api";
import { Card } from "./Card";
import { Loader } from "./Loader";
import SimpleBar from "simplebar-react";
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
                    <div
                        className={`transition-opacity ${
                            isFetching ? "opacity-100" : "opacity-0"
                        }`}
                    >
                        <Loader />
                    </div>
                </div>

                <SimpleBar
                    forceVisible="y"
                    autoHide={false}
                    clickOnTrack={false}
                    className="flex flex-col overflow-y-auto h-[320px]"
                >
                    {error && (
                        <div className="text-md py-2 px-4">
                            Error while fetching whitelist.
                        </div>
                    )}

                    <div
                        className={`flex flex-col transition-opacity ${
                            !error && users ? "opacity-100" : "opacity-0"
                        }`}
                    >
                        {users?.map((user: string) => {
                            return (
                                <div
                                    className="text-md odd:bg-gray-100 dark:odd:bg-gray-700 py-2 px-4 hover:bg-gray-200 dark:hover:bg-gray-500"
                                    key={user}
                                >
                                    {user}
                                </div>
                            );
                        })}
                    </div>
                </SimpleBar>

                <div className="h-4"></div>
            </div>
        </Card>
    );
}
