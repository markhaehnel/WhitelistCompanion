import React from "react";
import { useQuery } from "react-query";
import { fetchUserList } from "../api";
import { CopyButton } from "./CopyButton";
import { Loader } from "./Loader";

export function InfoText() {
    return (
        <div className="flex flex-row text-white items-center justify-center">
            <div className="text-2xl font-semibold drop-shadow-md">
                mc.noob-box.net
            </div>
            <div className="w-2"></div>
            <CopyButton value="mc.noob-box.net" />
        </div>
    );
}
