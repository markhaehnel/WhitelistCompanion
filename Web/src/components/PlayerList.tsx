import React from "react";
import { useQuery } from "react-query";
import { fetchUserList } from "../api";
import { Card } from "./Card";
import { Loader } from "./Loader";
import SimpleBar from "simplebar-react";

export function PlayerList() {
    const {
        data: data,
        error,
        isFetching,
    } = useQuery("userlist", async () => {
        let data = await fetchUserList();
        return data?.data;
    });

    return (
        <Card>
            <div className="">
                <div className="flex flex-row m-4">
                    <div className="text-xl">
                        Online{" "}
                        {data && (
                            <span className="font-thin">
                                ({data?.currentUserCount}/{data?.maxUserCount})
                            </span>
                        )}
                    </div>
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
                    className="flex flex-col overflow-y-auto h-[580px]"
                >
                    {error && (
                        <div className="text-md py-2 px-4">
                            Fehler beim Laden der aktuellen Spieler.
                        </div>
                    )}

                    <div
                        className={`flex flex-col transition-opacity ${
                            !error && data?.users ? "opacity-100" : "opacity-0"
                        }`}
                    >
                        {data?.users?.map((user: string) => {
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
                    {data?.users?.length === 0 && (
                        <div className="text-md odd:bg-gray-100 dark:odd:bg-gray-700 py-2 px-4 hover:bg-gray-200 dark:hover:bg-gray-500">
                            Keine Spieler online.
                        </div>
                    )}
                </SimpleBar>

                <div className="h-4"></div>
            </div>
        </Card>
    );
}
