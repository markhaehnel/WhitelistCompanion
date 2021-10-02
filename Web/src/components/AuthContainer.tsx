import React from "react";
import { useQuery } from "react-query";
import { fetchUserList, getQueryParam, HttpError } from "../api";
import { Card } from "./Card";
import { Loader } from "./Loader";
import { Logo } from "./Logo";

export function AuthContainer({ children }: { children: JSX.Element }) {
    const hasSecret = getQueryParam("secret");

    const { error, isFetching } = useQuery("auth", fetchUserList, {
        retry: 1,
        refetchOnWindowFocus: false,
        refetchInterval: false,
    });

    const httpError = error as HttpError;

    if (
        !hasSecret ||
        (hasSecret && httpError && httpError.statusCode === 401)
    ) {
        return (
            <>
                <Card error={true}>
                    <div className="text-lg p-2 text-center">
                        Nicht autorisiert!
                    </div>
                </Card>
            </>
        );
    } else if (httpError) {
        return (
            <>
                <Card error={true}>
                    <div className="text-lg p-2 text-center">
                        <span className="block">
                            Es ist ein unerwarteter Fehler aufgetreten.
                        </span>
                    </div>
                </Card>
            </>
        );
    } else if (isFetching && !error) {
        return (
            <div className="flex flex-col text-white justify-center items-center gap-4">
                <Loader />
            </div>
        );
    } else {
        return <>{children}</>;
    }
}
