import React, { useState } from "react";

export function CopyButton({ value }: { value: string }) {
    const [copied, setCopied] = useState(false);

    async function copyToClipboard() {
        await navigator.clipboard.writeText(value);
        setCopied(true);

        setTimeout(() => setCopied(false), 2000);
    }

    return (
        <div className="flex flex-row">
            <div
                className={`transition-all duration-300 ${
                    !copied ? "mr-0" : "-mr-6"
                }`}
            >
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    onClick={copyToClipboard}
                    className={`h-6 w-6 cursor-pointer text-gray-300 hover:text-gray-100 active:text-gray-400 dark:text-gray-400 dark:hover:text-gray-100 dark:active:text-gray-500 transition-all duration-300 ${
                        !copied ? "opacity-100 rotate-0" : "opacity-0 rotate-90"
                    }`}
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                >
                    <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth={2}
                        d="M8 16H6a2 2 0 01-2-2V6a2 2 0 012-2h8a2 2 0 012 2v2m-6 12h8a2 2 0 002-2v-8a2 2 0 00-2-2h-8a2 2 0 00-2 2v8a2 2 0 002 2z"
                    />
                </svg>
            </div>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                className={`h-6 w-6 transition-all duration-300 ${
                    copied ? "opacity-100 rotate-0" : "opacity-0 -rotate-90"
                }`}
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
            >
                <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M5 13l4 4L19 7"
                />
            </svg>
        </div>
    );
}
