import React from "react";
import { Logo } from "./components/Logo";
import { WhitelistList } from "./components/WhitelistList";
import { QueryClient, QueryClientProvider } from "react-query";
import { getQueryParam } from "./api";
import { Card } from "./components/Card";
import { InfoText } from "./components/InfoText";
import { WhitelistAddCard } from "./components/WhitelistAddCard";

const queryClient = new QueryClient();

function App() {
    const hasSecret = getQueryParam("secret") !== "";

    if (hasSecret) {
        return (
            <QueryClientProvider client={queryClient}>
                <div className="flex flex-row">
                    <Logo />
                    <div className="w-8"></div>
                    <InfoText />
                </div>
                <WhitelistAddCard />
                <WhitelistList />
            </QueryClientProvider>
        );
    } else {
        return (
            <Card error={true}>
                <div className="text-lg p-2 text-center">
                    Nicht autorisiert!
                </div>
            </Card>
        );
    }
}

export default App;
