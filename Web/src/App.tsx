import React from "react";
import { Logo } from "./components/Logo";
import { WhitelistAddForm } from "./components/WhitelistAddForm";
import { WhitelistList } from "./components/WhitelistList";
import { QueryClient, QueryClientProvider } from "react-query";
import { getSecretFromLocation } from "./api";
import { Card } from "./components/Card";
import { InfoText } from "./components/InfoText";

const queryClient = new QueryClient();

function App() {
    const hasSecret = getSecretFromLocation() !== "";

    return (
        <QueryClientProvider client={queryClient}>
            <div className="flex-row min-h-screen px-4">
                <div className="flex flex-col min-h-screen items-center justify-center gap-8 py-4">
                    <div className="flex flex-row">
                        <Logo />
                        <div className="w-8"></div>
                        <InfoText />
                    </div>
                    {hasSecret && (
                        <>
                            <WhitelistAddForm />
                            <WhitelistList />
                        </>
                    )}
                    {!hasSecret && (
                        <Card error={true}>
                            <div className="text-lg p-2 text-center">
                                Nicht autorisiert!
                            </div>
                        </Card>
                    )}
                </div>
            </div>
        </QueryClientProvider>
    );
}

export default App;
