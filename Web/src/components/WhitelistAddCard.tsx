import React, { useState } from "react";
import { Card } from "./Card";
import { WhitelistAddManualForm } from "./WhitelistAddManualForm";
import { WhitelistAddMicrosoftForm } from "./WhitelistAddMicrosoftForm";

export function WhitelistAddCard() {
    return (
        <Card>
            <>
                <WhitelistAddMicrosoftForm />
                <div className="flex flex-row mx-4">
                    <div className="flex-grow border-gray-200 dark:border-gray-700 border-b-[1px] mb-[10px]"></div>
                    <div className="text-sm text-center text-gray-400 dark:text-gray-600 mx-4">
                        oder manuell
                    </div>
                    <div className="flex-grow border-gray-200 dark:border-gray-700 border-b-[1px] mb-[10px]"></div>
                </div>
                <WhitelistAddManualForm />
            </>
        </Card>
    );
}
