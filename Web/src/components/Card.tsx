import React from "react";

export function Card({
    error = false,
    children,
}: {
    error?: boolean;
    children: JSX.Element;
}) {
    const bgColor = error ? "bg-red-600" : "bg-white";
    const fontColor = error ? "text-white" : "text-black";

    return (
        <div
            className={`flex flex-col ${bgColor} ${fontColor} rounded-lg shadow-lg max-w-full w-[512px]`}
        >
            {children}
        </div>
    );
}
