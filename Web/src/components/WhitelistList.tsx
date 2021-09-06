import React, { useState } from "react";
import { useQueries, useQuery } from "react-query";
import { fetchWhitelist } from "../api";
import { Card } from "./Card";
import { Loader } from "./Loader";

export function WhitelistList() {
    const ITEMS_PER_PAGE = 5;
    const [page, setPage] = useState(0);
    const {
        data: users,
        error,
        isFetching,
    } = useQuery("whitelist", async () => {
        setPage(0);
        let data = await fetchWhitelist();
        return data?.data?.users.sort((a: string, b: string) =>
            a.toLowerCase().localeCompare(b.toLowerCase())
        );
    });
    const pageStart = page * ITEMS_PER_PAGE;
    const pageEnd = pageStart + ITEMS_PER_PAGE;
    const maxPages = users ? Math.ceil(users.length / ITEMS_PER_PAGE) : 0;

    return (
        <Card>
            <div className="h-[324px]">
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
                    {!error && users && (
                        <div className="flex flex-col">
                            {users
                                .slice(pageStart, pageEnd)
                                .map((user: string) => {
                                    return (
                                        <div
                                            className="text-md odd:bg-gray-100 py-2 px-4 hover:bg-gray-200"
                                            key={user}
                                        >
                                            {user}
                                        </div>
                                    );
                                })}

                            <div className="flex flex-row mx-4 mt-4 gap-2">
                                {Array.from(Array(maxPages).keys()).map(
                                    (_, i) => {
                                        return (
                                            <button
                                                disabled={page === i}
                                                onClick={() => setPage(i)}
                                                key={i}
                                                className="text-center text-lg text-white bg-blue-600 border-2 border-transparent px-2 rounded-md shadow cursor-pointer hover:bg-blue-500 transition-transform active:bg-blue-600 active:scale-95 focus:border-gray-600 disabled:bg-blue-300 disabled:cursor-not-allowed;"
                                            >
                                                {i + 1}
                                            </button>
                                        );
                                    }
                                )}
                            </div>
                        </div>
                    )}
                </div>

                <div className="h-4"></div>
            </div>
        </Card>
    );
}
