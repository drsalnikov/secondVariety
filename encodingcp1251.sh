#!/bin/sh



while read -r ln
do
IsIso="`file -ikb $ln | grep 'iso-8859-1'`"

if [ -n "$IsIso" ]
then
Directory=$(dirname $ln)
FileName=$(basename $ln)
echo "!$IsIso! $ln <--- $Directory"
cat $ln
echo "Decode ..."
Decoded=$(cat $ln | iconv -f cp1251 -t utf8) ;
echo "$Decoded";
echo "Decoding to $Directory/$FileName.1"
cat $ln | iconv -f cp1251 -t utf8 > "$Directory/$FileName.1"
fi

done < <(find ./ -regex '^.*[cs|cshtml|js|razor]$'  -type f)
