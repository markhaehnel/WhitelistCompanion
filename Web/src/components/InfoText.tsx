import React from "react";
import { useQuery } from "react-query";
import { fetchUserList } from "../api";

export function InfoText() {
    const { data, error } = useQuery("userlist", async () => {
        let data = await fetchUserList();
        return data?.data;
    });

    return (
        <div className="flex flex-col text-white">
            <div className="text-2xl font-semibold drop-shadow-md">
                mc.noob-box.net
            </div>
            <div className="text-lg drop-shadow">
                {!error && data && (
                    <>
                        Spieler: {data.currentUserCount}/{data.maxUserCount}
                    </>
                )}
            </div>
        </div>
    );
}
