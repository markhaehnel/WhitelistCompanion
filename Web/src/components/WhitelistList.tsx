import * as React from "react";
import { useQuery } from "react-query";
import { fetchWhitelist } from "../api";
import { Card } from "./Card";
import SimpleBar from "simplebar-react";
import { Spinner } from "@chakra-ui/spinner";
import { Box, Flex, Spacer } from "@chakra-ui/layout";
import { Text, useColorModeValue } from "@chakra-ui/react";
import { ScrollableListContent } from "./ScrollableListContent";

export function WhitelistList() {
    const {
        data: users,
        error,
        isFetching,
    } = useQuery("whitelist", async () => {
        let data = await fetchWhitelist();
        return data?.data?.users.sort((a: string, b: string) =>
            a.toLowerCase().localeCompare(b.toLowerCase())
        );
    });

    return (
        <Card>
            <Flex p={4}>
                <Text fontSize="xl">Whitelist</Text>
                <Spacer />
                <Spinner
                    transition="150ms linear"
                    opacity={isFetching ? 100 : 0}
                />
            </Flex>

            <ScrollableListContent
                items={users as string[]}
                error={error ? "Fehler beim Laden der Whitelist." : undefined}
                empty="Keine Spieler auf der Whitelist."
                minMaxHeight="350px"
            />

            <Spacer h={4} />
        </Card>
    );
}
