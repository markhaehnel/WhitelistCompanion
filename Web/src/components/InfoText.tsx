import React from "react";
import { useQuery } from "react-query";
import { fetchUserList } from "../api";
import { CopyButton } from "./CopyButton";
import { Loader } from "./Loader";

export function InfoText() {
    const { data, error, isFetching } = useQuery("userlist", async () => {
        let data = await fetchUserList();
        return data?.data;
    });

    return (
        <div className="flex flex-col text-white items-center justify-center">
            <div className="flex flex-row items-center">
                <div className="text-2xl font-semibold drop-shadow-md">
                    mc.noob-box.net
                </div>
                <div className="w-2"></div>
                <CopyButton value="mc.noob-box.net" />
            </div>

            <div className="select-none flex flex-row items-center text-lg drop-shadow h-8">
                {data && (
                    <>
                        Spieler: {data?.currentUserCount}/{data?.maxUserCount}
                    </>
                )}
                {isFetching && !data && <Loader />}
            </div>
        </div>
    );
}
